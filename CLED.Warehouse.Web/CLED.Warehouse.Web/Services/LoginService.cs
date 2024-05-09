using CLED.Warehouse.Authentication;
using CLED.WareHouse.Models.Login;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace CLED.Warehouse.Web.Services;

public class LoginService
{
    private readonly ILogger<LoginService> _logger;
    private readonly IAuthManager _authManager;

    public LoginService(ILogger<LoginService> logger, IAuthManager authManager)
    {
        _authManager = authManager;
        _logger = logger;
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

            var tokenExpDate = DateTime.UtcNow.AddHours(24);
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
        //if (handler is null)
        //{
        //	handler = new JwtSecurityTokenHandler();
        //}
        //var key = Encoding.UTF8.GetBytes(_secretSettings.Secret);

        //var tokenDescription = new SecurityTokenDescriptor
        //{
        //	Subject = new ClaimsIdentity(
        //		new Claim[]
        //		{
        //		new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(CultureInfo.InvariantCulture)),
        //		new Claim(AuthenticationExtensions.ApplicationClaimName, structure.Id.ToString(CultureInfo.InvariantCulture)),
        //		GetRoleClaim(structClaims, RoleNameConsts.CashflowAdminRole),
        //		GetRoleClaim(structClaims, RoleNameConsts.AdminRole),
        //		GetRoleClaim(structClaims, RoleNameConsts.TutorRole),
        //		GetRoleClaim(structClaims, RoleNameConsts.ParentRole),
        //		}.Concat(structClaims.Select(c => new Claim(c.Code, string.Empty)))
        //	),
        //	Expires = expiration,
        //	SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        //};

        //var token = handler.CreateToken(tokenDescription);

        //return handler.WriteToken(token);
        return string.Empty;
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
