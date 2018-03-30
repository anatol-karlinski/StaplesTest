namespace Staples.Adapters.Interfaces
{
    public interface IAdapter<T, U>
    {
        U Adapt(T source, U target);
        U Adapt(T source);

        T Adapt(U source, T target);
        T Adapt(U source);
    }
}
