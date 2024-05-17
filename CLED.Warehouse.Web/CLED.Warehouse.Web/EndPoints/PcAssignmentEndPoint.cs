using CLED.Warehouse.Models.DB;
using CLED.WareHouse.Services.DBServices.PcServices;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CLED.Warehouse.Web.EndPoints;

public static class PcAssignmentEndPoint
{
    public static IEndpointRouteBuilder MapPcAssignmentPoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/v1/pcAssignment")
            // .RequireAuthorization()
            .WithTags("PcAssignments");

        group.MapGet("/", GetAllPcAssignmentAsync)
            .WithName("GetPAssignments")
            .WithSummary("Get all Summary")
            .WithDescription("Return a list of all pcs");
        
        group.MapGet("/{id:int}", GetPcAssignmentByIdAsync)
            .WithName("GetPcAssignmentsById")
            .WithSummary("Get all Summary")
            .WithDescription("Return a single pc selected by ID");

        group.MapPost("/create", InsertPcAssignmentAsync)
            .WithName("InsertPcAssignment")
            .WithSummary("Create a new summary")
            .WithDescription("Insert the new pc's values inside json file");

        group.MapPut("/{id:int}", UpdatePcAssignmentAsync)
            .WithName("UpdatePcAssignment")
            .WithSummary("Update the Pc")
            .WithDescription("Change pc's values inside json file");

        group.MapDelete("/{id:int}", DeletePcAssignmentAsync)
            .WithName("DeletePcAssignment")
            .WithSummary("Delete the Pc");

        return builder;
    }

    private static async Task<Ok<IEnumerable<PcAssignment>>> GetAllPcAssignmentAsync(PcAssignmentService data)
    {
        var list = await data.GetAll();
        return TypedResults.Ok((list));
    }

    private static async Task<Results<Ok<PcAssignment>, NotFound>> GetPcAssignmentByIdAsync(int id, PcAssignmentService data)
    {
        var product =  await data.GetById(id);
        if (product == null)
            return TypedResults.NotFound();
    
        return TypedResults.Ok(product);
    }

    private static async Task<Created> InsertPcAssignmentAsync(PcAssignment pcAssignment, PcAssignmentService data)
    {
        await data.Insert(pcAssignment);
        return TypedResults.Created();
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