using CLED.Warehouse.Models.DB;
using CLED.WareHouse.Services.DBServices.PcServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CLED.Warehouse.Web.EndPoints;

public static class PcAssignmentEndPoint
{
    public static IEndpointRouteBuilder MapPcAssignmentPoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/v1/pcAssignment")
            .RequireAuthorization();

        group.MapGet("/", GetAllPcAssignmentAsync);
        group.MapGet("/{id:int}", GetPcAssignmentByIdAsync);
        group.MapGet("/student", GetStudentAssignments);
        group.MapGet("/return", ReturnPc);

        group.MapPost("/create", InsertPcAssignmentAsync);

        group.MapPut("/{id:int}", UpdatePcAssignmentAsync);

        group.MapDelete("/{id:int}", DeletePcAssignmentAsync);
            


		return builder;
    }

    private static async Task<Ok<IEnumerable<PcAssignment>>> GetAllPcAssignmentAsync(PcAssignmentService data)
    {
        var list = await data.GetAll();
        return TypedResults.Ok((list));
    }

    private static async Task<Ok<IEnumerable<PcAssignment>>> GetStudentAssignments([FromQuery]int studentId, PcAssignmentService data)
	{
		var list = await data.GetStudentAssignments(studentId);
		return TypedResults.Ok(list);
	}

	private static async Task<Results<Ok<PcAssignment>, NotFound>> GetPcAssignmentByIdAsync(int id, PcAssignmentService data)
    {
        var product =  await data.GetById(id);
        if (product == null)
            return TypedResults.NotFound();
    
        return TypedResults.Ok(product);
    }

    private static async Task<Created> InsertPcAssignmentAsync([FromQuery]bool isNewPc, [FromBody]PcAssignment pcAssignment, PcAssignmentService data, PcService pcService)
    {
        if (!isNewPc)
            await data.Insert(pcAssignment);
        else
        {
            var pc = await pcService.Insert(pcAssignment.Pc);
            pcAssignment.PcId = pc.Id;
			pcAssignment.Pc = null;
            await data.Insert(pcAssignment);
		}

		return TypedResults.Created();
    }

    private static async Task<Ok<bool>> ReturnPc([FromQuery]int assignmentId, [FromQuery]DateTime returnDate, [FromQuery]int returnReasonId, PcAssignmentService service)
    {
		return TypedResults.Ok(await service.ReturnPc(assignmentId, returnDate, returnReasonId));
	}

    private static async Task<Results<NoContent, NotFound>> UpdatePcAssignmentAsync(int id, PcAssignment pcAssignment, PcAssignmentService data)
    {
        var temp = await data.GetById(id);
        if (temp == null)
            return TypedResults.NotFound();

        pcAssignment.Id = id;
        await data.Update(pcAssignment);
        return TypedResults.NoContent();
    }

    private static async Task<Results<NoContent, NotFound>> DeletePcAssignmentAsync(int id, PcAssignmentService data)
    {
        var temp = await data.GetById(id);
        if (temp == null)
            return TypedResults.NotFound();
        
        await data.Delete(id);
        return TypedResults.NoContent();
    }
}