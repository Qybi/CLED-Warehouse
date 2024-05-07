using CLED.Warehouse.Models.DB;
using CLED.WareHouse.Services.DBServices.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CLED.WareHouse.Services.DBServices;

public class TicketService : IService<Ticket>
{
    private readonly string _connectionString;

    public TicketService(IConfiguration? configuration)
    {
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
        
        await connection.ExecuteAsync(query, new {id = ticketId});
    }
}