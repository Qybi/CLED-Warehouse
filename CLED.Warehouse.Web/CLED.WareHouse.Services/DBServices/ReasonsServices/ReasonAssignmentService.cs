using CLED.WareHouse.Models.Database.Reasons;
using CLED.WareHouse.Services.DBServices.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CLED.WareHouse.Services.DBServices.ReasonsServices;

public class ReasonAssignmentService : IService<ReasonsAssignment>
{
    private readonly string _connectionString;

    public ReasonAssignmentService(IConfiguration? configuration)
    {
        _connectionString = configuration.GetConnectionString("db");
    }
    public async Task<ReasonsAssignment> GetById(int reasonAssignmentId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       SELECT Id,
                              Name
                       FROM "ReasonsAssignment"
                       WHERE Id = @id;
                       """;
        
        return await connection.QueryFirstOrDefaultAsync<ReasonsAssignment>(query, new { id = reasonAssignmentId });
    }

    public async Task<IEnumerable<ReasonsAssignment>> GetAll()
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       SELECT Id,
                              Name
                       FROM "ReasonsAssignment";
                       """;
        
        return await connection.QueryAsync<ReasonsAssignment>(query);
    }

    public async Task Insert(ReasonsAssignment reasonsAssignment)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        
        string query = """
                       INSERT INTO "ReasonsAssignment" (Id, Name)
                       VALUES (@Id, @Name);
                       """;
        
        await connection.ExecuteAsync(query, reasonsAssignment);
    }

    public async Task Update(ReasonsAssignment reasonsAssignment)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       UPDATE "ReasonsAssignment" SET
                           Id = @Id,
                           Name = @Name
                       WHERE Id = @Id;
                       """;
        
        await connection.ExecuteAsync(query, reasonsAssignment);
    }

    public async Task Delete(int reasonAssignmentId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       DELETE FROM "ReasonsAssignment" WHERE Id = @id;
                       """;
        
        await connection.ExecuteAsync(query, new {id = reasonAssignmentId});
    }
}