using System.Threading.Tasks;

namespace Staples.DAL.Interfaces
{
    public interface IXmlDbHelper<T> where T : class
    {
        Task<int> AddAsync(T entity);
        Task<int> RemoveAsync(T entity);
    }
}