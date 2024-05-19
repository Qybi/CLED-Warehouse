using CLED.Warehouse.Models.DB;
using CLED.WareHouse.Services.DBServices.AccessoryServices;
using CLED.WareHouse.Services.DBServices.Interfaces;
using CLED.WareHouse.Services.DBServices.PcServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CLED.Warehouse.Web;

public static class AccessoryAssignmentEndPoint
{
	public static IEndpointRouteBuilder MapAccessoryAssignmentEndPoint(this IEndpointRouteBuilder builder)
	{
		var group = builder.MapGroup("api/v1/accessoryAssignment")
			.RequireAuthorization();

		group.MapGet("/", GetAllAccessoryAssignmentAsync);
		group.MapGet("/{id:int}", GetAccessoryAssignmentByIdAsync);
		group.MapGet("/student", GetStudentAssignments);
		group.MapGet("/return", ReturnAccessoryAsync);
		group.MapGet("/accessoryData", GetAssingnmentsByAccessoryId);

		group.MapPost("/create", InsertAccessoryAssignmentAsync);

		group.MapPut("/{id:int}", UpdateAccessoryAssignmentAsync);

		group.MapDelete("/{id:int}", DeleteAccessoryAssignmentAsync);

		return builder;
	}

	private static async Task<Ok<IEnumerable<AccessoriesAssignment>>> GetAllAccessoryAssignmentAsync(AccessoryAssignmentService data)
	{
		var list = await data.GetAll();
		return TypedResults.Ok((list));
	}

	private static async Task<Ok<IEnumerable<AccessoriesAssignment>>> GetStudentAssignments([FromQuery] int studentId, AccessoryAssignmentService data)
	{
		var list = await data.GetStudentAssignments(studentId);
		return TypedResults.Ok(list);
	}

	private static async Task<Results<Ok<AccessoriesAssignment>, NotFound>> GetAccessoryAssignmentByIdAsync(int id, AccessoryAssignmentService data)
	{
		var product = await data.GetById(id);
		if (product == null)
			return TypedResults.NotFound();

		return TypedResults.Ok(product);
	}

	private static async Task<Created> InsertAccessoryAssignmentAsync([FromBody]AccessoriesAssignment accessoryAssignment, AccessoryAssignmentService data)
	{
		await data.Insert(accessoryAssignment);
		return TypedResults.Created();
	}

	private static async Task<Results<NoContent, NotFound>> UpdateAccessoryAssignmentAsync(int id, AccessoriesAssignment accessoryAssignment, AccessoryAssignmentService data)
	{
		var temp = await data.GetById(id);
		if (temp == null)
			return TypedResults.NotFound();

		accessoryAssignment.Id = id;
		await data.Update(accessoryAssignment);
		return TypedResults.NoContent();
	}

	private static async Task<Results<NoContent, NotFound>> DeleteAccessoryAssignmentAsync(int id, AccessoryAssignmentService data)
	{
		var temp = await data.GetById(id);
		if (temp == null)
			return TypedResults.NotFound();

		await data.Delete(id);
		return TypedResults.NoContent();
	}

	private static async Task<Ok<bool>> ReturnAccessoryAsync([FromQuery]int assignmentId, [FromQuery]DateTime returnDate, [FromQuery]int returnReasonId, AccessoryAssignmentService service)
	{
		return TypedResults.Ok(await service.ReturnAccessory(assignmentId, returnDate, returnReasonId));
	}

	private static async Task<Ok<IEnumerable<AccessoriesAssignment>>> GetAssingnmentsByAccessoryId(int accessoryId, AccessoryAssignmentService data)
	{
		return TypedResults.Ok(await data.GetAssignmentsByAccessoryId(accessoryId));
	}
}