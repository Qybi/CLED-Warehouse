using CLED.Warehouse.Authentication.Utils.Abstractions;
using CLED.Warehouse.Models.DB;
using CLED.WareHouse.Models.Login;
using Dapper;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace CLED.Warehouse.Authentication;

public class AuthManager : IAuthManager
{
	private readonly ILogger<AuthManager> _logger;
	private readonly IHashingUtils _hashingUtility;
	private readonly string _connectionString;

	public AuthManager(ILogger<AuthManager> logger, IHashingUtils hashingUtility, string connectionString)
	{
		_logger = logger;
		_hashingUtility = hashingUtility;
		_connectionString = connectionString;
	}

	// using dapper right here, since in the future we might migrate the user data to a different database
	// and handling the connection with dapper is easier than with EF for this specific case
	public async Task<UserInfo> GetUserAsync(int id)
	{
		using NpgsqlConnection connection = new(_connectionString);
		await connection.OpenAsync();
		string query =
			"""
				SELECT Username, PasswordHash as Password, PasswordSalt as Salt, Enabled, Roles
				FROM users
				WHERE id = @id;
			""";
		return await connection.QueryFirstOrDefaultAsync<UserInfo>(query, new { id });
	}

	public async Task<UserInfo> GetUserAsync(string username)
	{
		using NpgsqlConnection connection = new(_connectionString);
		await connection.OpenAsync();
		string query =
			"""
				SELECT Username, PasswordHash as Password, PasswordSalt as Salt, Enabled, Roles
				FROM users
				WHERE username = @username;
			""";
		return await connection.QueryFirstOrDefaultAsync<UserInfo>(query, new { username });
	}

	public async Task<LoginResponse> LoginAsync(LoginAttempt login)
	{
		if (login is null || string.IsNullOrWhiteSpace(login.Username) || string.IsNullOrWhiteSpace(login.Password))
		{
			_logger.LogWarning("Someone submitted an invalid login attempt: {login}", login);
			return new LoginResponse
			{
				IsSuccessful = false,
				LoginStatus = LoginStatus.Invalid,
				Message = "Invalid login attempt. All the fields should be specified."
			};
		}

		var userInfo = await GetUserAsync(login.Username);

		if (userInfo is null)
		{
			return new LoginResponse
			{
				IsSuccessful = false,
				LoginStatus = LoginStatus.Unauthorized,
				Message = "Username or password incorrect."
			};
		}

		if (!userInfo.Enabled)
		{
			return new LoginResponse
			{
				IsSuccessful = false,
				LoginStatus = LoginStatus.Locked,
				Message = $"Your account is locked. Contact administration"
			};
		}

		string attemptLoginHash = _hashingUtility.GetHashedPassword(login.Password, userInfo.Salt).PasswordHash;

		if (attemptLoginHash.Equals(userInfo.Password))
		{
			return new LoginResponse
			{
				IsSuccessful = true,
				LoginStatus = LoginStatus.Successful,
				Message = "Login success.",
				User = userInfo
			};
		}
		else
		{
			return new LoginResponse
			{
				IsSuccessful = false,
				LoginStatus = LoginStatus.Unauthorized,
				Message = "Username or password incorrect."
			};
		}
	}

	public async Task RegisterAsync(RegisterAttempt register)
	{
		if (register is null || string.IsNullOrWhiteSpace(register.Username) || string.IsNullOrWhiteSpace(register.Password))
		{
			_logger.LogWarning("Someone submitted an invalid register attempt: {register}", register);
			throw new ArgumentException("Invalid register attempt. All the fields should be specified.");
		}

		var userInfo = await GetUserAsync(register.Username);

		if (userInfo is not null)
		{
			throw new ArgumentException("Username already exists.");
		}

		var hashedPassword = _hashingUtility.GetHashedPassword(register.Password);

		using NpgsqlConnection connection = new(_connectionString);
		await connection.OpenAsync();
		string query =
			"""
				INSERT INTO users (Username, PasswordHash, PasswordSalt, Enabled, Roles)
				VALUES (@Username, @Password, @Salt, @Enabled, @Roles);
			""";
		await connection.ExecuteAsync(query, new
		{
			Username = register.Username,
			Password = hashedPassword.PasswordHash,
			Salt = hashedPassword.Salt,
			Enabled = true,
			Roles = register.Roles.ToArray()
		});
	}
}

