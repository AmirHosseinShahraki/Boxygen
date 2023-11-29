namespace Shared;

public interface IBaseRepository<T>
{
    public Task<T?> GetById(Guid id);
    public Task<T> Create(T entity);
    public Task<bool> Update(Guid id, T updatedEntity);
    public Task<bool> Delete(Guid id);
}