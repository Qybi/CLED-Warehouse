using CLED.WareHouse.Models.Database;
using CLED.WareHouse.Services.DBServices.Interfaces;

namespace CLED.WareHouse.Services.DBServices;

public class CourseService : IService<Course>
{
    public async Task<Course> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Course>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task Insert(Course course)
    {
        throw new NotImplementedException();
    }

    public async Task Update(Course course)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(int id)
    {
        throw new NotImplementedException();
    }
}