using CLED.Warehouse.Web.Services;
using CLED.WareHouse.Models.Login;
using CLED.WareHouse.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CLED.Warehouse.Web.EndPoints;

public static class LoginEndpoint
{
	public static IEndpointRouteBuilder MapLoginEndPoints(this IEndpointRouteBuilder builder)
	{
		var group = builder.MapGroup("api/v1/login")
			.AllowAnonymous();

		group.MapPost("/", LoginAsync);

		return builder;
	}

	private static async Task<Results<Ok<LoginResponse>, UnauthorizedHttpResult>> LoginAsync(LoginService service, [FromBody]LoginAttempt loginAttempt)
	{
		try
		{
			var lr = await service.Login(loginAttempt);

			if (lr.IsSuccessful) return TypedResults.Ok(lr);
			else return TypedResults.Unauthorized();
		}
		catch (Exception ex)
		{
			throw;
		}
	}
}
