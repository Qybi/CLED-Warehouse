using CLED.WareHouse.Models.Database.Reasons;
using CLED.WareHouse.Services.DBServices.ReasonsServices;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CLED.Warehouse.Web.EndPoints;

public static class ReasonAssignmentEndPoint
{
    public static IEndpointRouteBuilder MapReasonAssignmentEndPoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/v1/reasonAssignment")
            .RequireAuthorization()
            .WithTags("ReasonAssignment");

        group.MapGet("/", GetAllReasonAssignmentAsync)
            .WithName("GetReasonAssignment")
            .WithSummary("Get all Summary")
            .WithDescription("Return a list of all Reason Assignment");
        
        group.MapGet("/{id:int}", GetReasonAssignmentByIdAsync)
            .WithName("GetrAasonAssignmentById")
            .WithSummary("Get all Summary")
            .WithDescription("Return a single reasonAssignment selected by ID");

        group.MapPost("/", InsertReasonAssignmentAsync)
            .WithName("InsertReasonAssignment")
            .WithSummary("Create a new summary")
            .WithDescription("Insert the new reasonAssignment values inside json file");

        group.MapPut("/{id:int}", UpdateReasonAssignmentAsync)
            .WithName("UpdateReasonAssignment")
            .WithSummary("Update the reasonAssignment")
            .WithDescription("Change reasonAssignment values inside json file");

        group.MapDelete("/{id:int}", DeleteReasonAssignmentAsync)
            .WithName("DeleteReasonAssignment")
            .WithSummary("Delete the reasonAssignment");

        return builder;
    }

    private static async Task<Ok<IEnumerable<ReasonsAssignment>>> GetAllReasonAssignmentAsync(ReasonAssignmentService data)
    {
        var list = await data.GetAll();
        return TypedResults.Ok((list));
    }

    private static async Task<Results<Ok<ReasonsAssignment>, NotFound>> GetReasonAssignmentByIdAsync(int id, ReasonAssignmentService data)
    {
        var product =  await data.GetById(id);
        if (product == null)
            return TypedResults.NotFound();
    
        return TypedResults.Ok(product);
    }

    private static async Task<Created> InsertReasonAssignmentAsync(ReasonsAssignment reasonsAssignment, ReasonAssignmentService data)
    {
        await data.Insert(reasonsAssignment);
        return TypedResults.Created();
    }

    private static async Task<Results<NoContent, NotFound>> UpdateReasonAssignmentAsync(int id, ReasonsAssignment reasonsAssignment, ReasonAssignmentService data)
    {
        var temp = await data.GetById(id);
        if (temp == null)
            return TypedResults.NotFound();

        reasonsAssignment.Id = id;
        await data.Update(reasonsAssignment);
        return TypedResults.NoContent();
    }

    private static async Task<Results<NoContent, NotFound>> DeleteReasonAssignmentAsync(int id, ReasonAssignmentService data)
    {
        var temp = await data.GetById(id);
        if (temp == null)
            return TypedResults.NotFound();
        
        await data.Delete(id);
        return TypedResults.NoContent();
    }
}