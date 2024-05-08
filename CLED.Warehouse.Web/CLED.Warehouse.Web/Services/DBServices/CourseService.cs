using CLED.Warehouse.Models.DB;
using CLED.Warehouse.Web;
using CLED.WareHouse.Services.DBServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace CLED.WareHouse.Services.DBServices;

public class CourseService : IService<Course>
{
    private readonly WarehouseContext _context;
    private readonly ILogger<CourseService> _logger;

    public CourseService(ILogger<CourseService> logger, WarehouseContext context)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<Course> GetById(int courseId)
    {
        try
        {
            return await _context.Courses.FindAsync(courseId);
        }
        catch (Exception ex)
        {
			_logger.LogError(ex, "GetAll method has thrown an exception");
			throw;
        }
    }

    public async Task<IEnumerable<Course>> GetAll()
    {
        try
        {

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetAll method has thrown an exception");
			throw;
        }
        return await _context.Courses.ToListAsync();
    }

    public async Task Insert(Course course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Course course)
    {
        try
        {
            var c = await _context.Courses.FindAsync(course.Id);
            // write me the code to update the course entity

            c.FullName = course.FullName;
		    c.ShortName = course.ShortName;
		    c.DateStart = course.DateStart;
		    c.DateEnd = course.DateEnd;
            c.Code = course.Code;
            c.Location = course.Location;
            c.Status = course.Status;
            _context.Courses.Update(c);
        
		    await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
			_logger.LogError(ex, "Update method has thrown an exception");
			throw;
        }
    }

    public async Task Delete(int courseId)
    {
        _context.Remove(await _context.Courses.FindAsync(courseId));
        await _context.SaveChangesAsync();
    }
}