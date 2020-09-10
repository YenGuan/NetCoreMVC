
namespace NetCoreIdentity.Model
{
    public interface IRepositoryFactory
    {
        IRepository<T> GetRepository<T>() where T : class;
 
    }
}
