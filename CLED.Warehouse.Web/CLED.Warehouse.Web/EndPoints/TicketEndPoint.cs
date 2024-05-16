using CLED.Warehouse.Models.DB;
using CLED.WareHouse.Services.DBServices;
using CLED.WareHouse.Services.DBServices.PcServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CLED.Warehouse.Web.EndPoints;

public static class TicketEndPoint
{
    public static IEndpointRouteBuilder MapTicketEndPoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/v1/ticket")
            // .RequireAuthorization()
            .WithTags("Ticket");

        group.MapGet("/", GetAllTicketAsync)
            .WithName("GetTickets")
            .WithSummary("Get all Summary")
            .WithDescription("Return a list of all tickets");
        
        group.MapGet("/{id:int}", GetTicketByIdAsync)
            .WithName("GetTicketById")
            .WithSummary("Get all Summary")
            .WithDescription("Return a single ticket selected by ID");

        group.MapGet("/open", GetAllOpenTicketsAsync)
            .WithName("GetTicketOpen")
            .WithSummary("Get all Summary")
            .WithDescription("Return a single ticket selected by ID");
        
        group.MapPost("/", InsertTicketAsync)
            .WithName("InsertTicket")
            .WithSummary("Create a new summary")
            .WithDescription("Insert the new tickets values inside json file");

        group.MapPut("/{id:int}", UpdateTicketAsync)
            .WithName("UpdateTicket")
            .WithSummary("Update the Ticket")
            .WithDescription("Change tickets values inside json file");

        group.MapDelete("/{id:int}", DeleteTicketAsync)
            .WithName("DeleteTicket")
            .WithSummary("Delete the Ticket");

        group.MapPut("/closeTicket", CloseTicketAsync);

        group.MapGet("/checkSerialPC", CheckSerialPcAsync);

        return builder;
    }

    private static async Task<Ok<IEnumerable<Ticket>>> GetAllTicketAsync(TicketService data)
    {
        var list = await data.GetAll();
        return TypedResults.Ok((list));
    }
    
    private static async Task<Ok<IEnumerable<Ticket>>> GetAllOpenTicketsAsync(TicketService data)
    {
        var list = await data.GetOpenTicket();
        return TypedResults.Ok((list));
    }

    private static async Task<Results<Ok<Ticket>, NotFound>> GetTicketByIdAsync(int id, TicketService data)
    {
        var product =  await data.GetById(id);
        if (product == null)
            return TypedResults.NotFound();
    
        return TypedResults.Ok(product);
    }

    private static async Task<Created> InsertTicketAsync(Ticket ticket, TicketService data)
    {
        await data.Insert(ticket);
        return TypedResults.Created();
    }

    private static async Task<Results<NoContent, NotFound>> UpdateTicketAsync(int id, Ticket ticket, TicketService data)
    {
        var temp = await data.GetById(id);
        if (temp == null)
            return TypedResults.NotFound();

        ticket.Id = id;
        await data.Update(ticket);
        return TypedResults.NoContent();
    }

    private static async Task<Results<NoContent, NotFound>> DeleteTicketAsync(int id, TicketService data)
    {
        var temp = await data.GetById(id);
        if (temp == null)
            return TypedResults.NotFound();
        
        await data.Delete(id);
        return TypedResults.NoContent();
    }

    private static async Task<Results<NoContent, NotFound>> CloseTicketAsync(int id, string status, TicketService data, Ticket ticket)
    {
        var temp = await data.GetById(id);
        if (temp == null)
            return TypedResults.NotFound();

        ticket.Id = id;
        await data.SetStatus(id, status);
        return TypedResults.NoContent();
    }

    private static async Task<Results<Ok<bool>, NotFound>> CheckSerialPcAsync([FromQuery] string serial, TicketService data)
    {
        var temp = await data.CheckSerial(serial);
        if (temp == null)
            return TypedResults.NotFound();
        return TypedResults.Ok(temp);
    }
}