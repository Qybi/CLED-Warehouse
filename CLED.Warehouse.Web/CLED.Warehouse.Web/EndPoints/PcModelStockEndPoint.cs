using CLED.WareHouse.Models.Database.PCs;
using CLED.WareHouse.Services.DBServices.PcServices;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CLED.Warehouse.Web.EndPoints;

public static class PcModelStockEndPoint
{
    public static IEndpointRouteBuilder MapPcModelStockEndPoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/v1/pcModelStock")
            .RequireAuthorization()
            .WithTags("PcModelStock");

        group.MapGet("/", GetAllPcModelStockAsync)
            .WithName("GetPcsModelStock")
            .WithSummary("Get all Summary")
            .WithDescription("Return a list of all pcs");
        
        group.MapGet("/{id:int}", GetPcModelStockByIdAsync)
            .WithName("GetPcModelStockById")
            .WithSummary("Get all Summary")
            .WithDescription("Return a single pc selected by ID");

        group.MapPost("/", InsertPcModelStockAsync)
            .WithName("InsertPcModelStock")
            .WithSummary("Create a new summary")
            .WithDescription("Insert the new pc's values inside json file");

        group.MapPut("/{id:int}", UpdatePcModelStockAsync)
            .WithName("UpdatePcModelStock")
            .WithSummary("Update the PcModelStock")
            .WithDescription("Change pc's values inside json file");

        group.MapDelete("/{id:int}", DeletePcModelStockAsync)
            .WithName("DeletePcModelStock")
            .WithSummary("Delete the PcModelStock");

        return builder;
    }

    private static async Task<Ok<IEnumerable<PcModelStock>>> GetAllPcModelStockAsync(PcModelStockService data)
    {
        var list = await data.GetAll();
        return TypedResults.Ok((list));
    }

    private static async Task<Results<Ok<PcModelStock>, NotFound>> GetPcModelStockByIdAsync(int id, PcModelStockService data)
    {
        var product =  await data.GetById(id);
        if (product == null)
            return TypedResults.NotFound();
    
        return TypedResults.Ok(product);
    }

    private static async Task<Created> InsertPcModelStockAsync(PcModelStock pcModelStock, PcModelStockService data)
    {
        await data.Insert(pcModelStock);
        return TypedResults.Created();
    }

    private static async Task<Results<NoContent, NotFound>> UpdatePcModelStockAsync(int id, PcModelStock pcModelStock, PcModelStockService data)
    {
        var temp = await data.GetById(id);
        if (temp == null)
            return TypedResults.NotFound();

        pcModelStock.Id = id;
        await data.Update(pcModelStock);
        return TypedResults.NoContent();
    }

    private static async Task<Results<NoContent, NotFound>> DeletePcModelStockAsync(int id, PcModelStockService data)
    {
        var temp = await data.GetById(id);
        if (temp == null)
            return TypedResults.NotFound();
        
        await data.Delete(id);
        return TypedResults.NoContent();
    }
}