using CLED.Warehouse.Models.DB;
using CLED.WareHouse.Services.DBServices.PcServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;

namespace CLED.Warehouse.Web.EndPoints;
public static class PcEndPoint
{
	public static IEndpointRouteBuilder MapPcEndPoints(this IEndpointRouteBuilder builder)
	{
		var group = builder.MapGroup("api/v1/pc")
			.RequireAuthorization()
			.WithTags("Pc");

		group.MapGet("/", GetAllPcAsync)
			.WithName("GetPcs")
			.WithSummary("Get all Summary")
			.WithDescription("Return a list of all pcs");

		group.MapGet("/{id:int}", GetPcByIdAsync)
			.WithName("GetPcById")
			.WithSummary("Get all Summary")
			.WithDescription("Return a single pc selected by ID");

		group.MapPost("/", InsertPcAsync)
			.WithName("InsertPc")
			.WithSummary("Create a new summary")
			.WithDescription("Insert the new pc's values inside json file");

		group.MapPut("/update", UpdatePcAsync)
			.WithName("UpdatePc")
			.WithSummary("Update the Pc")
			.WithDescription("Change pc's values inside json file")
			.WithTags("PC");

		group.MapDelete("/{id:int}", DeletePcAsync)
			.WithName("DeletePc")
			.WithSummary("Delete the Pc");

		group.MapGet("/checkSerialPC", CheckSerialPcAsync);
		
		group.MapPost("/insertSerial", InsertNewSerialAsync);

		group.MapGet("/getPcIdFromSerial", GetPcFromSerialAsync);

		return builder;
	}

	private static async Task<Ok<IEnumerable<Pc>>> GetAllPcAsync(PcService data)
	{
		var list = await data.GetAll();
		return TypedResults.Ok((list));
	}

	private static async Task<Results<Ok<Pc>, NotFound>> GetPcByIdAsync(int id, PcService data)
	{
		var product = await data.GetById(id);
		if (product == null)
			return TypedResults.NotFound();

		return TypedResults.Ok(product);
	}

	private static async Task<Created> InsertPcAsync(Pc pc, PcService data)
	{
		await data.Insert(pc);
		return TypedResults.Created();
	}

	private static async Task<Results<NoContent, NotFound>> UpdatePcAsync([FromQuery] int id, [FromBody] Pc pc, PcService data)
	{
		var temp = await data.GetById(id);
		if (temp == null)
			return TypedResults.NotFound();

		pc.Id = id;
		await data.Update(pc);
		return TypedResults.NoContent();
	}

	private static async Task<Results<NoContent, NotFound>> DeletePcAsync(int id, PcService data)
	{
		var temp = await data.GetById(id);
		if (temp == null)
			return TypedResults.NotFound();

		await data.Delete(id);
		return TypedResults.NoContent();
	}
	
	

	private static async Task<Created> InsertNewSerialAsync(Pc pc, PcService data)
	{
		await data.InsertFromNewSerial(pc);
		return TypedResults.Created();
	}
	
	private static async Task<Results<Ok<bool>, NotFound>> CheckSerialPcAsync([FromQuery] string serial, PcService data)
	{
		var temp = await data.CheckSerial(serial);
		if (temp == null)
			return TypedResults.NotFound();
		return TypedResults.Ok(temp);
	}

	private static async Task<Results<Ok<Pc>, NotFound>> GetPcFromSerialAsync(PcService data, [FromQuery] string serial)
	{
		var temp = await data.GetPcFromSerial(serial);
		return TypedResults.Ok(temp);
	}
}