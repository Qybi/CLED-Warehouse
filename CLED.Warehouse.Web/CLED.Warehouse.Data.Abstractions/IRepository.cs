using CLED.WareHouse.Models.DBDecoratorInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CLED.Warehouse.Data.Abstractions;

public interface IRepository<TKey, TEntity> where TEntity : class, IIdentifiable<TKey>
{
	Task<TEntity> GetAsync(TKey key);
	Task<IEnumerable<TEntity>> GetAllAsync();

	Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

	Task AddAsync(TEntity entity);
	Task AddRangeAsync(IEnumerable<TEntity> entities);

	Task DeleteAsync(TEntity entity);
	Task DeleteAsync(TKey key);

	Task UpdateAsync(TEntity entity);
}
