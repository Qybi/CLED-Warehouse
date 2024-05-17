using CLED.Warehouse.Models.DB;
using CLED.Warehouse.Web;
using CLED.WareHouse.Services.DBServices.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;
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
        return await _context.Courses.ToListAsync();
	}

    public async Task Insert(Course course)
    {
        try
        {
            course.RegistrationUser = -1;
		    course.RegistrationDate = DateTime.Now;
		    _context.Courses.Add(course);
		    await _context.SaveChangesAsync();

        }
        catch (Exception ex)
        {

            throw;
        }
	}

    public async Task Update(Course course)
    {
        try
        {
            var cToUpdate = await _context.Courses.FindAsync(course.Id);

            cToUpdate.Location = course.Location;
		    cToUpdate.DateStart = course.DateStart;
		    cToUpdate.DateEnd = course.DateEnd;
            cToUpdate.Code = course.Code;
		    cToUpdate.FullName = course.FullName;
            cToUpdate.Status = course.Status;
            cToUpdate.ShortName = course.ShortName;

            _context.Courses.Update(cToUpdate);
		    await _context.SaveChangesAsync();

        }
        catch (Exception ex)
        {

            throw;
        }
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