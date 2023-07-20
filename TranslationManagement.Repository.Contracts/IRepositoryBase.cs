using System.Threading.Tasks;

namespace TranslationManagement.Repository.Contracts
{
    public interface IRepositoryBase<T>
    {
        Task Add(T entity);
    }
}
