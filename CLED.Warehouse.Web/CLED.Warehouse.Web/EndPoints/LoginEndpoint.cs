using CLED.WareHouse.Models.Login;
using CLED.WareHouse.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CLED.Warehouse.Web.EndPoints;

public class LoginEndpoint
{
	public static IEndpointRouteBuilder MapLoginEndPoints(this IEndpointRouteBuilder builder)
	{
		var group = builder.MapGroup("api/v1/login")
			.AllowAnonymous()
			.WithTags("Login");

		group.MapPost("/", LoginAsync)
			.WithName("Login")
			.WithSummary("Login")
			.WithDescription("Login to the system");

		return builder;
	}

	private static async Task<Ok<IEnumerable<Ticket>>> LoginAsync(LoginService data)
	{
	}
}
