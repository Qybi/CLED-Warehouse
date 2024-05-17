using CLED.WareHouse.Models.Constants;
using CLED.Warehouse.Models.DB;
using CLED.WareHouse.Services.DBServices.Interfaces;
using CLED.Warehouse.Web;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CLED.WareHouse.Services.DBServices;

public class TicketService : IService<Ticket>
{
    private readonly string _connectionString;
    private readonly WarehouseContext _context;

    public TicketService(IConfiguration? configuration, WarehouseContext context)
    {
        _context = context;
        _connectionString = configuration.GetConnectionString("db");
    }

    public async Task<Ticket> GetById(int ticketId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       SELECT "Id",
                              "StudentId",
                              "TicketType",
                              "TicketBody",
                              "Status",
                              "DateOpen",
                              "DateClose",
                              "UserClaimOpen",
                              "UserClaimClose",
                              "RegistrationDate",
                              "DeletedDate",
                              "DeletedUser"
                       FROM "Tickets"
                       WHERE "Id" = @id;
                       """;

        return await connection.QueryFirstOrDefaultAsync<Ticket>(query, new { id = ticketId });
    }

    public async Task<IEnumerable<Ticket>> GetAll()
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       SELECT "Id",
                              "StudentId",
                              "TicketType",
                              "TicketBody",
                              "Status",
                              "DateOpen",
                              "DateClose",
                              "UserClaimOpen",
                              "UserClaimClose",
                              "RegistrationDate",
                              "DeletedDate",
                              "DeletedUser"
                       FROM "Tickets";
                       """;

        return await connection.QueryAsync<Ticket>(query);
    }

    public async Task Insert(Ticket ticket)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       INSERT INTO "Tickets" ("Id", "StudentId", "TicketType", "TicketBody", "Status", "DateOpen", "DateClose", "UserClaimOpen", "UserClaimClose", "RegistrationDate", "DeletedDate", "DeletedUser")
                       VALUES (@Id, @StudentId, @TicketType, @TicketBody, @Status, @DateOpen, @DateClose, @UserClaimOpen, @UserClaimClose, @RegistrationDate, @DeletedDate, @DeletedUser);
                       """;

        await connection.ExecuteAsync(query, ticket);
    }

    public async Task Update(Ticket ticket)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       UPDATE "Tickets" SET
                           "Id" = @Id,
                           "StudentId" = @StudentId,
                           "TicketType" = @TicketType,
                           "TicketBody" = @TicketBody,
                           "Status" = @Status,
                           "DateOpen" = @DateOpen,
                           "DateClose" = @DateClose,
                           "UserClaimOpen" = @UserClaimOpen,
                           "UserClaimClose" = @UserClaimClose,
                           "RegistrationDate" = @RegistrationDate,
                           "DeletedDate" = @DeletedDate,
                           "DeletedUser" = @DeletedUser
                       WHERE "Id" = @Id;
                       """;

        await connection.ExecuteAsync(query, ticket);
    }

    public async Task Delete(int ticketId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       DELETE FROM "Tickets" WHERE "Id" = @id;
                       """;

        await connection.ExecuteAsync(query, new { id = ticketId });
    }

    public async Task<IEnumerable<Ticket>> GetOpenTicket()
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       SELECT "Id",
                              "StudentId",
                              "TicketType",
                              "TicketBody",
                              "Status",
                              "DateOpen",
                              "DateClose",
                              "UserClaimOpen",
                              "UserClaimClose",
                              "RegistrationDate",
                              "DeletedDate",
                              "DeletedUser"
                       FROM "Tickets"
                       WHERE "Status" = "OPEN";
                       """;

        return await connection.QueryAsync<Ticket>(query);
    }

    public async Task SetStatus(int id, string status)
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == id);
        ticket.Status = TicketStatus.Closed;

        await _context.SaveChangesAsync();
    }
    
    // public async Task<bool> CheckSerial(string serial)
    // {
    //     try
    //     {
    //         return await _context.Pcs.AnyAsync(x => x.Serial == serial); //anyasync come ifExists, fa select ifexists where condition
    //     }
    //     catch (Exception ex)
    //     {
    //
    //         throw;
    //     }
    // }

    public async Task<bool> CheckCespite(int cespite)
    {
        try
        {
            return await _context.Pcs.AnyAsync(x => x.StockId == cespite);
        }
        catch (Exception)
        {
            throw;
        }
    }
}