using CLED.WareHouse.Models.Database;
using CLED.WareHouse.Models.Database.PCs;
using CLED.WareHouse.Services.DBServices.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CLED.WareHouse.Services.DBServices.PcServices;

public class PcAssignmentService : IService<PcAssignment>
{
    private readonly string _connectionString;

    public PcAssignmentService(IConfiguration? configuration)
    {
        _connectionString = configuration.GetConnectionString("db");
    }
    public async Task<PcAssignment> GetById(int pcAssignmentId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       SELECT "Id", 
                              "PCId", 
                              "StudentId", 
                              "AssignmentDate", 
                              "AssignmentReasonId", 
                              "IsReturned", 
                              "ForecastedReturnDate", 
                              "ActualReturnDate", 
                              "ReturnReasonId", 
                              "RegistrationDate", 
                              "RegistrationUser", 
                              "DeletedDate", 
                              "DeletedUser"
                       FROM "PCAssignments"
                       WHERE "Id" = @id;
                       """;
        
        return await connection.QueryFirstOrDefaultAsync<PcAssignment>(query, new { id = pcAssignmentId });
    }

    public async Task<IEnumerable<PcAssignment>> GetAll()
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       SELECT "Id", 
                              "PCId", 
                              "StudentId", 
                              "AssignmentDate", 
                              "AssignmentReasonId", 
                              "IsReturned", 
                              "ForecastedReturnDate", 
                              "ActualReturnDate", 
                              "ReturnReasonId", 
                              "RegistrationDate", 
                              "RegistrationUser", 
                              "DeletedDate", 
                              "DeletedUser"
                       FROM "PCAssignments"
                       """;
        
        return await connection.QueryAsync<PcAssignment>(query);
    }

    public async Task Insert(PcAssignment pcAssignment)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        
        string query = """
                       INSERT INTO "PCAssignments" ("Id", "PCId", "StudentId", "AssignmentDate", "AssignmentReasonId", "IsReturned", "ForecastedReturnDate", "ActualReturnDate", "ReturnReasonId", "RegistrationDate", "RegistrationUser", "DeletedDate", 2)
                       VALUES (@Id, @PCId, @StudentId, @AssignmentDate, @AssignmentReasonId, @IsReturned, @ForecastedReturnDate, @ActualReturnDate, @ReturnReasonId, @RegistrationDate, @RegistrationUser, @DeletedDate, @DeletedUser);
                       """;
        
        await connection.ExecuteAsync(query, pcAssignment);
    }

    public async Task Update(PcAssignment pcAssignment)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       UPDATE "PCAssignments" SET
                           "Id" = @Id,
                           "PCId" = @PCId,
                           "StudentId" = @StudentId,
                           "AssignmentDate" = @AssignmentDate,
                           "AssignmentReasonId" = @AssignmentReasonId,
                           "IsReturned" = @IsReturned,
                           "ForecastedReturnDate" = @ForecastedReturnDate,
                           "ActualReturnDate" = @ActualReturnDate,
                           "ReturnReasonId" = @ReturnReasonId,
                           "RegistrationDate" = @RegistrationDate,
                           "RegistrationUser" = @RegistrationUser,
                           "DeletedDate" = @DeletedDate,
                           "DeletedUser" = @DeletedUser
                       WHERE "Id" = @Id;
                       """;
        
        await connection.ExecuteAsync(query, pcAssignment);
    }

    public async Task Delete(int pcAssignmentId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       DELETE FROM "PCAssignments" WHERE "Id" = @id;
                       """;
        
        await connection.ExecuteAsync(query, new {id = pcAssignmentId});
    }
}