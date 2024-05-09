using CLED.Warehouse.Models.DB;
using CLED.Warehouse.Web;
using CLED.WareHouse.Services.DBServices.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CLED.WareHouse.Services.DBServices;

public class CourseService : IService<Course>
{
    private readonly string _connectionString;
    private readonly WarehouseContext _context;

    public CourseService(IConfiguration? configuration, WarehouseContext context)
    {
        _connectionString = configuration.GetConnectionString("db");
        _context = context;
    }
    public async Task<Course> GetById(int courseId)
    {
        return await _context.Courses.FindAsync(courseId);
    }

    public async Task<IEnumerable<Course>> GetAll()
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       SELECT "Id", 
                              "Code", 
                              "FullName", 
                              "DateStart", 
                              "DateEnd", 
                              "RegistrationDate", 
                              "RegistrationUser", 
                              "DeletedDate", 
                              "DeletedUser"
                       FROM "Courses";
                       """;
        
        return await connection.QueryAsync<Course>(query);
    }

    public async Task Insert(Course course)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        
        string query = """
                       INSERT INTO "Courses" ("Id", "Code", "FullName", "DateStart", "DateEnd", "RegistrationDate", "RegistrationUser", "DeletedDate", "DeletedUser")
                       VALUES (@Id, @Code, @FullName, @DateStart, @DateEnd, @RegistrationDate, @RegistrationUser, @DeletedDate, @DeletedUser);
                       """;
        
        await connection.ExecuteAsync(query, course);
    }

    public async Task Update(Course course)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       UPDATE "Courses" SET
                           "Id" = @Id,
                           "Code" = @Code,
                           "FullName" = @FullName,
                           "DateStart" = @DateStart,
                           "DateEnd" = @DateEnd,
                           "RegistrationDate" = @RegistrationDate,
                           "RegistrationUser" = @RegistrationUser,
                           "DeletedDate" = @DeletedDate,
                           "DeletedUser" = @DeletedUser
                       WHERE "Id" = @Id;
                       """;
        
        await connection.ExecuteAsync(query, course);
    }

    public async Task Delete(int courseId)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = """
                       DELETE FROM "Courses" WHERE "Id" = @id;
                       """;
        
        await connection.ExecuteAsync(query, new {id = courseId});
    }
}