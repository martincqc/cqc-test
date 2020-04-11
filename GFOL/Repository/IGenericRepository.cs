using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GFOL.Data;
using GFOL.Models;

namespace GFOL.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> CreateAsync(T item);
        Task DeleteAsync(int id);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        Task<T> UpdateAsync(int id, T item);
    }
}
