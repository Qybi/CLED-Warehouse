using CLED.WareHouse.Models.Database.Reasons;
using CLED.WareHouse.Services.DBServices.ReasonsServices;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CLED.Warehouse.Web.EndPoints;

public static class ReasonReturnEndPoint
{
    public static IEndpointRouteBuilder MapReasonReturnEndPoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/v1/reasonReturn")
            .WithTags("ReasonReturn");

        group.MapGet("/", GetAllReasonReturnAsync)
            .WithName("GetReasonReturn")
            .WithSummary("Get all Summary")
            .WithDescription("Return a list of all ReasonReturn");
        
        group.MapGet("/{id:int}", GetReasonReturnByIdAsync)
            .WithName("GetReasonReturnById")
            .WithSummary("Get all Summary")
            .WithDescription("Return a single ReasonReturn selected by ID");

        group.MapPost("/", InsertReasonReturnAsync)
            .WithName("InsertReasonReturn")
            .WithSummary("Create a new summary")
            .WithDescription("Insert the new ReasonReturn values inside json file");

        group.MapPut("/{id:int}", UpdateReasonReturnAsync)
            .WithName("UpdateReasonReturn")
            .WithSummary("Update the ReasonReturn")
            .WithDescription("Change ReasonReturn values inside json file");

        group.MapDelete("/{id:int}", DeleteReasonReturnAsync)
            .WithName("DeleteReasonReturn")
            .WithSummary("Delete the ReasonReturn");

        return builder;
    }

    private static async Task<Ok<IEnumerable<ReasonsReturn>>> GetAllReasonReturnAsync(ReasonReturnService data)
    {
        var list = await data.GetAll();
        return TypedResults.Ok((list));
    }

    private static async Task<Results<Ok<ReasonsReturn>, NotFound>> GetReasonReturnByIdAsync(int id, ReasonReturnService data)
    {
        var product =  await data.GetById(id);
        if (product == null)
            return TypedResults.NotFound();
    
        return TypedResults.Ok(product);
    }

    private static async Task<Created> InsertReasonReturnAsync(ReasonsReturn reasonsReturn, ReasonReturnService data)
    {
        await data.Insert(reasonsReturn);
        return TypedResults.Created();
    }

    private static async Task<Results<NoContent, NotFound>> UpdateReasonReturnAsync(int id, ReasonsReturn reasonsReturn, ReasonReturnService data)
    {
        var temp = await data.GetById(id);
        if (temp == null)
            return TypedResults.NotFound();

        reasonsReturn.Id = id;
        await data.Update(reasonsReturn);
        return TypedResults.NoContent();
    }

    private static async Task<Results<NoContent, NotFound>> DeleteReasonReturnAsync(int id, ReasonReturnService data)
    {
        var temp = await data.GetById(id);
        if (temp == null)
            return TypedResults.NotFound();
        
        await data.Delete(id);
        return TypedResults.NoContent();
    }
}