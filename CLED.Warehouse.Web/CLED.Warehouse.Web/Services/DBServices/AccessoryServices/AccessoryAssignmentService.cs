﻿using CLED.Warehouse.Models.DB;
using CLED.WareHouse.Services.DBServices.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CLED.WareHouse.Services.DBServices.AccessoryServices;

public class AccessoryAssignmentService : IService<AccessoriesAssignment>
{
    
    private readonly string _connectionString;

    public AccessoryAssignmentService(IConfiguration? configuration)
    {
        _connectionString = configuration.GetConnectionString("db");
    }
    public async Task<AccessoriesAssignment> GetById(int accessoryAssignmentId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();   
        
        string query = """
                       SELECT 
                           "Id",
                           "AccessoryId",
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
                       FROM "AccessoriesAssignments"
                       WHERE "Id" = @id;
                       """;
        
        return await connection.QueryFirstOrDefaultAsync<AccessoriesAssignment>(query, new { id = accessoryAssignmentId });
    }

    public async Task<IEnumerable<AccessoriesAssignment>> GetAll()
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();   
        
        string query = """
                       SELECT
                           "Id",
                           "AccessoryId",
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
                       FROM "AccessoriesAssignments";
                       """;
        
        return await connection.QueryAsync<AccessoriesAssignment>(query);
    }

    public async Task Insert(AccessoriesAssignment accessoryAssignment)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        
        string query = """
                       INSERT INTO "AccessoriesAssignments" ("Id", "AccessoryId", "StudentId", "AssignmentDate", "AssignmentReasonId", "IsReturned", "ForecastedReturnDate", "ActualReturnDate", "ReturnReasonId", "RegistrationDate", "RegistrationUser", "DeletedDate", "DeletedUser")
                       VALUES (@Id, @AccessoryId, @StudentId, @AssignmentDate, @AssignmentReasonId, @IsReturned, @ForecastedReturnDate, @ActualReturnDate, @ReturnReasonId, @RegistrationDate, @RegistrationUser, @DeletedDate, @DeletedUser);
                       """;
        await connection.ExecuteAsync(query, accessoryAssignment);
    }

    public async Task Update(AccessoriesAssignment accessoryAssignment)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       UPDATE "AccessoriesAssignments" SET 
                       "Id" = @Id,
                       "AccessoryId" = @AccessoryId,
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
                       WHERE 2 = @Id;
                       """;
        
        await connection.ExecuteAsync(query, accessoryAssignment);
    }

    public async Task Delete(int accessoryAssignmentId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       DELETE FROM "AccessoriesAssignments" WHERE "Id" = @id;
                       """;
        
        await connection.ExecuteAsync(query, new {id = accessoryAssignmentId});
    }
}