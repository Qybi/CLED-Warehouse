using CLED.Warehouse.Models.DB;
using CLED.WareHouse.Services.DBServices.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CLED.WareHouse.Services.DBServices.ReasonsServices;

public class ReasonReturnService : IService<ReasonsReturn>
{
    private readonly string _connectionString;

    public ReasonReturnService(IConfiguration? configuration)
    {
        _connectionString = configuration.GetConnectionString("db");
    }
    public async Task<ReasonsReturn> GetById(int reasonReturnId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       SELECT "Id", 
                              "Name"
                       FROM "ReasonsReturn"
                       WHERE "Id" = @id;
                       """;
        
        return await connection.QueryFirstOrDefaultAsync<ReasonsReturn>(query, new { id = reasonReturnId });
    }

    public async Task<IEnumerable<ReasonsReturn>> GetAll()
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       SELECT "Id", 
                              "Name"
                       FROM "ReasonsReturn";
                       """;
        
        return await connection.QueryAsync<ReasonsReturn>(query);
    }

    public async Task Insert(ReasonsReturn reasonsReturn)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        
        string query = """
                       INSERT INTO "ReasonsReturn" ("Id", "Name")
                       VALUES (@Id, @Name);
                       """;
        
        await connection.ExecuteAsync(query, reasonsReturn);
    }

    public async Task Update(ReasonsReturn reasonsReturn)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       UPDATE "ReasonsReturn" SET
                           "Id" = @Id,
                           "Name" = @Name
                       WHERE "Id" = @Id;
                       """;
        
        await connection.ExecuteAsync(query, reasonsReturn);
    }

    public async Task Delete(int reasonReturnId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       DELETE FROM "ReasonsReturn" WHERE "Id" = @id;
                       """;
        
        await connection.ExecuteAsync(query, new {id = reasonReturnId});
    }
}