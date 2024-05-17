using CLED.Warehouse.Authentication;
using CLED.Warehouse.Models.DB;
using CLED.WareHouse.Models.Login;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CLED.Warehouse.Web.Services;

public class LoginService
{
    private readonly ILogger<LoginService> _logger;
    private readonly IAuthManager _authManager;
    private readonly IConfiguration _config;
    private readonly WarehouseContext _context;

	public LoginService(ILogger<LoginService> logger, IAuthManager authManager, IConfiguration config, WarehouseContext context)
	{
		_authManager = authManager;
		_logger = logger;
		_config = config;
		_context = context;
	}
	public async Task<LoginResponse> Login(LoginAttempt login)
    {
        if (login is null || string.IsNullOrWhiteSpace(login.Username) || string.IsNullOrWhiteSpace(login.Password))
        {

            _logger.LogWarning(
                "Someone submitted an invalid login attempt. Username: {Login} - StructureId: {Structure}",
                login?.Username ?? string.Empty
            );
            return new LoginResponse()
            {
                IsSuccessful = false,
                Message = "The username or the password are not correct, please try again"
            };
        }

        try
        {
            LoginResponse loginResponse = await _authManager.LoginAsync(login);

			if (loginResponse is null || !loginResponse.IsSuccessful || loginResponse.User is null || !loginResponse.User.Enabled)
            {
                return new LoginResponse()
                {
                    IsSuccessful = false,
                    Message = "The username or the password are not correct, please try again"
                };
            }
			int? studentId = (await _context.Students.FirstOrDefaultAsync(s => s.UserId == loginResponse.User.Id))?.Id;

            var tokenExpDate = DateTime.UtcNow.AddHours(2);
            //tokenExpDate = new DateTime(tokenExpDate.Year, tokenExpDate.Month, tokenExpDate.Day, 2, 0, 0);

            _logger.LogInformation("User {username} logged.", loginResponse.User.Username);

            //ritorno OK con il token
            return new LoginResponse()
            {
                IsSuccessful = true,
                Message = "Login successful",
                Credentials = new Credentials()
                {
                    Token = await GetToken(loginResponse.User, tokenExpDate, studentId),
                    TokenExpDate = tokenExpDate,
                    Username = loginResponse.User.Username,
                    Roles = loginResponse.User.Roles.ToArray(),
					StudentId = studentId
				}
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error while retrieving the token for the user {Username} and structure {Structure}",
                login.Username
            );
            throw;
        }
    }

    public async Task RegisterAsync(RegisterAttempt registerAttempt)
    {
        if (string.IsNullOrEmpty(registerAttempt.Username) || string.IsNullOrEmpty(registerAttempt.Password))
            return;

        try
        {
            await _authManager.RegisterAsync(registerAttempt);
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    private async Task<string> GetToken(UserInfo user, DateTime expiration, int? studentId = null, SecurityTokenHandler handler = null)
    {
        if (handler is null)
        {
            handler = new JwtSecurityTokenHandler();
        }
        var key = Encoding.UTF8.GetBytes(_config.GetValue<string>("Jwt:Key") ?? string.Empty);

        Claim claim;

		if (user.Roles.Select(x => x.ToLower()).Contains("admin"))
            claim = new Claim(ClaimTypes.Role, "admin");
        else
            claim = new Claim(ClaimTypes.Role, "user");

        var tokenDescription = new SecurityTokenDescriptor
        {
			Subject = new ClaimsIdentity(
                new Claim[]
                {
					new Claim(ClaimTypes.NameIdentifier, user.Username.ToString(CultureInfo.InvariantCulture)),
					claim
				}
            ),
            Expires = expiration,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

		if (studentId.HasValue)
		{
			tokenDescription.Subject.AddClaim(new Claim("studentId", studentId.Value.ToString()));
		}

		var token = handler.CreateToken(tokenDescription);

        return handler.WriteToken(token);
    }

    private Claim GetRoleClaim(List<UserClaim> claims, string claim)
    {
        var claimobj = claims.Find(e => e.Code == claim);
        if (claimobj is null)
        {
            return null;
        }
        claims.Remove(claimobj);
        return new Claim(ClaimTypes.Role, claim);
    }
}
