namespace Staples.DAL.Interfaces
{
    public interface ILogHelper
    {
        void LogEntity<T>(T entity) where T : class;
    }
}