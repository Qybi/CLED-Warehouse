using CLED.Warehouse.Authentication;
using CLED.WareHouse.Models.Login;
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

	public LoginService(ILogger<LoginService> logger, IAuthManager authManager, IConfiguration config)
	{
		_authManager = authManager;
		_logger = logger;
		_config = config;
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
                Message = "The username or the password are not correct, please try again",
                LoginStatus = LoginStatus.Unauthorized
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
                    Message = "The username or the password are not correct, please try again",
                    LoginStatus = LoginStatus.Unauthorized
                };
            }

            var tokenExpDate = DateTime.UtcNow.AddHours(2);
            tokenExpDate = new DateTime(tokenExpDate.Year, tokenExpDate.Month, tokenExpDate.Day, 2, 0, 0);

            _logger.LogInformation("User {username} logged.", loginResponse.User.Username);

            //ritorno OK con il token
            return new LoginResponse()
            {
                IsSuccessful = true,
                Message = "Login successful",
                LoginStatus = LoginStatus.Successful,
                Credentials = new Credentials()
                {
                    Token = GetToken(loginResponse.User, tokenExpDate),
                    TokenExpDate = tokenExpDate,
                    Username = loginResponse.User.Username,
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

    private string GetToken(UserInfo user, DateTime expiration, SecurityTokenHandler handler = null)
    {
        if (handler is null)
        {
            handler = new JwtSecurityTokenHandler();
        }
        var key = Encoding.UTF8.GetBytes(_config.GetValue<string>("Jwt:Key") ?? string.Empty);

        var tokenDescription = new SecurityTokenDescriptor
        {
			Subject = new ClaimsIdentity(
                new Claim[]
                {
					new Claim(ClaimTypes.NameIdentifier, user.Username.ToString(CultureInfo.InvariantCulture)),
					user.Roles.Contains("admin") ? new Claim(ClaimTypes.Role, "admin") : new Claim(ClaimTypes.Role, "user"),
                    //new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(CultureInfo.InvariantCulture)),
                    //GetRoleClaim(structClaims, RoleNameConsts.AdminRole)
                }
            ),
            Expires = expiration,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

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
