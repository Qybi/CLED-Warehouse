using CLED.WareHouse.Models.Database;
using CLED.WareHouse.Services.DBServices.Interfaces;

namespace CLED.WareHouse.Services.DBServices;

public class StudentService : IService<Student>
{
    public Task<Student> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Task<Student>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task Insert(Student student)
    {
        throw new NotImplementedException();
    }

    public Task Update(Student student)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }
}