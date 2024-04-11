using CLED.WareHouse.Models.Database;
using CLED.WareHouse.Services.DBServices.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CLED.WareHouse.Services.DBServices;

public class StudentService : IService<Student>
{
    private readonly string _connectionString;

    public StudentService(IConfiguration? configuration)
    {
        _connectionString = configuration.GetConnectionString("db");
    }
    
    public async Task<Student> GetById(int studentId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       SELECT "Id", 
                              "SchoolIdentifier", 
                              "Email", 
                              "Surname", 
                              "Name", 
                              "CourseId", 
                              "DateOfBirth", 
                              "FiscalCode", 
                              "Gender", 
                              "RegistrationDate", 
                              "RegistrationUser", 
                              "DeletedDate", 
                              "DeletedUser"
                       FROM "Students"
                       WHERE "Id" = @id;
                       """;
        
        return await connection.QueryFirstOrDefaultAsync<Student>(query, new { id = studentId });
    }

    public async Task<IEnumerable<Student>> GetAll()
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       SELECT "Id", 
                              "SchoolIdentifier", 
                              "Email", 
                              "Surname", 
                              "Name", 
                              "CourseId", 
                              "DateOfBirth", 
                              "FiscalCode", 
                              "Gender", 
                              "RegistrationDate", 
                              "RegistrationUser", 
                              "DeletedDate", 
                              "DeletedUser"
                       FROM "Students";
                       """;
        
        return await connection.QueryAsync<Student>(query);
    }

    public async Task Insert(Student student)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        
        string query = """
                       INSERT INTO "Students" ("Id", "SchoolIdentifier", "Email", "Surname", "Name", "CourseId", "DateOfBirth", "FiscalCode", "Gender", "RegistrationDate", "RegistrationUser", "DeletedDate", "DeletedUser")
                       VALUES (@Id, @SchoolIdentifier, @Email, @Surname, @Name, @CourseId, @DateOfBirth, @FiscalCode, @Gender, @RegistrationDate, @RegistrationUser, @DeletedDate, @DeletedUser);
                       """;
        
        await connection.ExecuteAsync(query, student);
    }

    public async Task Update(Student student)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       UPDATE "Students" SET
                           "Id" = @Id,
                           "SchoolIdentifier" = @SchoolIdentifier,
                           "Email" = @Email,
                           "Surname" = @Surname,
                           "Name" = @Name,
                           "DateOfBirth" = @DateOfBirth,
                           "FiscalCode" = @FiscalCode,
                           "Gender" = @Gender,
                           "UserClaimClose" = @UserClaimClose,
                           "RegistrationDate" = @RegistrationDate,
                           "RegistrationUser" = @RegistrationUser,
                           "DeletedDate" = @DeletedDate,
                           "DeletedUser" = @DeletedUser
                       WHERE "Id" = @Id;
                       """;
        
        await connection.ExecuteAsync(query, student);
    }

    public async Task Delete(int studentId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       DELETE FROM "Students" WHERE "Id" = @id;
                       """;
        
        await connection.ExecuteAsync(query, new {id = studentId});
    }
}