using LabTec.Common.Utilities;

namespace LabTec.Common.Interfaces
{
    public interface IRepository<T>
    {
        Task<Result<T>> GetByIdAsync(Guid id);
        Task<Result<IEnumerable<T>>> GetAllAsync();
        Task<Result<T>> AddAsync(T entity);
        Task<Result<T>> UpdateAsync(T entity);
        Task<Result> DeleteAsync(T entity);
    }
}
