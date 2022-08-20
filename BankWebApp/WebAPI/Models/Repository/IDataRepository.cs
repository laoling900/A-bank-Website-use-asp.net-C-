

    namespace BankWebAdmin.Models.Repository;

    public interface IDataRepository<TEntity, Tkey>where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(Tkey id);
        Tkey Add(TEntity item);
        Tkey Update(Tkey id,TEntity item);
        Tkey Delete(Tkey id);
    }

