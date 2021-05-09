namespace Infrastructure.DependencyInjection
{
    public interface IDependencyResolver
    {
        T Resolve<T>();
    }
}