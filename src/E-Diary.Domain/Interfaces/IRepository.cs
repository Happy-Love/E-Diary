namespace E_Diary.Domain.Entities.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        ValueTask<T?> GetById(object id);
        Task<T[]> List(ISpecification<T> specification, int skip = 0, int take = 100);
    }
}
