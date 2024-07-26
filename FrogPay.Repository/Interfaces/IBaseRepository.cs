using FrogPay.Domain.Entities;
using System.Linq.Expressions;

namespace FrogPay.Repository.Interfaces
{
    public interface IBaseRepository<T> where T : Base
    {
        Task<T> CreateAsync(T obj);
        Task<T> UpdateAsync(T obj);
        Task RemoveAsync(Guid id);
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(Guid id);
        Task<T> GetAsync(Expression<Func<T, bool>> expression, bool asNoTracking = true);
        Task<IList<T>> SearchAsync(Expression<Func<T, bool>> expression, bool asNoTracking = true);
    }
}
