namespace CLED.WareHouse.Services.DBServices.Interfaces;


public interface IService<T>
{
    Task<T> GetById(int id);

    IEnumerable<Task<T>> GetAll();

    Task Insert(T obj);

    Task Update(T obj);

    Task Delete(int id);

}