using CLED.WareHouse.Services.DBServices.AccessoryServices;
using CLED.WareHouse.Models.Database.Accessories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CLED.Warehouse.Web;

public static class AccessoryAssignmentEndPoint
{
    public static IEndpointRouteBuilder MapAccessoryAssignmentEndPoint(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/v1/accessoryAssignment")
            .RequireAuthorization()
            .WithTags("accessoryAssignment");

        group.MapGet("/", GetAllAccessoryAssignmentAsync)
            .WithName("GetAccessoryAssignment")
            .WithSummary("Get all Summary")
            .WithDescription("Return a list of all accessory assignment");
        
        group.MapGet("/{id:int}", GetAccessoryAssignmentByIdAsync)
            .WithName("GetAccessoryAssignmentById")
            .WithSummary("Get all Summary")
            .WithDescription("Return a single accessory assignment selected by ID");

        group.MapPost("/", InsertAccessoryAssignmentAsync)
            .WithName("InsertAccessoryAssignment")
            .WithSummary("Create a new summary")
            .WithDescription("Insert the new accessory assignment's values inside json file");

        group.MapPut("/{id:int}", UpdateAccessoryAssignmentAsync)
            .WithName("UpdateAccessoryAssignment")
            .WithSummary("Update the Accessory Assignment")
            .WithDescription("Change Accessory Assignment values inside json file");

        group.MapDelete("/{id:int}", DeleteAccessoryAssignmentAsync)
            .WithName("DeleteAccessoryAssignment")
            .WithSummary("Delete the Accessory Assignment");

        return builder;
    }

    private static async Task<Ok<IEnumerable<AccessoryAssignment>>> GetAllAccessoryAssignmentAsync(
        AccessoryAssignmentService data)
    {
        var list = await data.GetAll();
        return TypedResults.Ok((list));
    }

    private static async Task<Results<Ok<AccessoryAssignment>, NotFound>> GetAccessoryAssignmentByIdAsync(int id, AccessoryAssignmentService data)
    {
        var product =  await data.GetById(id);
        if (product == null)
            return TypedResults.NotFound();
    
        return TypedResults.Ok(product);
    }

    private static async Task<Created> InsertAccessoryAssignmentAsync(AccessoryAssignment accessoryAssignment, AccessoryAssignmentService data)
    {
        await data.Insert(accessoryAssignment);
        return TypedResults.Created();
    }

    private static async Task<Results<NoContent, NotFound>> UpdateAccessoryAssignmentAsync(int id, AccessoryAssignment accessoryAssignment, AccessoryAssignmentService data)
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
}