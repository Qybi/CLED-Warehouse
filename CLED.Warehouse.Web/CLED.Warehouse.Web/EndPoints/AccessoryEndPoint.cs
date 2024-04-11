using CLED.WareHouse.Models.Database.Accessories;
using CLED.WareHouse.Services.DBServices.AccessoryServices;
using CLED.WareHouse.Services.DBServices.PcServices;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CLED.Warehouse.Web.EndPoints;

public static class AccessoryEndPoint
{
    public static IEndpointRouteBuilder MapAccessoryEndPoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/v1/accessory").RequireAuthorization()
            .WithTags("accessory");

        group.MapGet("/", GetAllAccessoryAsync)
            .WithName("GetAccessories")
            .WithSummary("Get all Summary")
            .WithDescription("Return a list of all accessories");
        
        group.MapGet("/{id:int}", GetAccessoryByIdAsync)
            .WithName("GetAccessoryById")
            .WithSummary("Get all Summary")
            .WithDescription("Return a single accessory selected by ID");

        group.MapPost("/", InsertAccessoryAsync)
            .WithName("InsertAccessory")
            .WithSummary("Create a new summary")
            .WithDescription("Insert the new accessories values inside json file");

        group.MapPut("/{id:int}", UpdateAccessoryAsync)
            .WithName("UpdateAccessory")
            .WithSummary("Update the Accessory")
            .WithDescription("Change accessory values inside json file");

        group.MapDelete("/{id:int}", DeleteAccessoryAsync)
            .WithName("DeleteAccessory")
            .WithSummary("Delete the Accessory");

        return builder;
    }

    private static async Task<Ok<IEnumerable<Accessory>>> GetAllAccessoryAsync(AccessoryService data)
    {
        var list = await data.GetAll();
        return TypedResults.Ok((list));
    }

    private static async Task<Results<Ok<Accessory>, NotFound>> GetAccessoryByIdAsync(int id, AccessoryService data)
    {
        var product =  await data.GetById(id);
        if (product == null)
            return TypedResults.NotFound();
    
        return TypedResults.Ok(product);
    }

    private static async Task<Created> InsertAccessoryAsync(Accessory accessory, AccessoryService data)
    {
        await data.Insert(accessory);
        return TypedResults.Created();
    }

    private static async Task<Results<NoContent, NotFound>> UpdateAccessoryAsync(int id, Accessory accessory, AccessoryService data)
    {
        var temp = await data.GetById(id);
        if (temp == null)
            return TypedResults.NotFound();

        accessory.Id = id;
        await data.Update(accessory);
        return TypedResults.NoContent();
    }

    private static async Task<Results<NoContent, NotFound>> DeleteAccessoryAsync(int id, AccessoryService data)
    {
        var temp = await data.GetById(id);
        if (temp == null)
            return TypedResults.NotFound();
        
        await data.Delete(id);
        return TypedResults.NoContent();
    }
}