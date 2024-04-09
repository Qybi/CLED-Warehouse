using CLED.WareHouse.Models.Database;
using CLED.WareHouse.Services.DBServices.Interfaces;

namespace CLED.WareHouse.Services.DBServices;

public class StudentService : IService<Student>
{
    public async Task<Student> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Student>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task Insert(Student student)
    {
        throw new NotImplementedException();
    }

    public async Task Update(Student student)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(int id)
    {
        throw new NotImplementedException();
    }
}