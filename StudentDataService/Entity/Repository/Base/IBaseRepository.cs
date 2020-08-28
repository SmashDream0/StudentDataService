using StudentDataService.Entity.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Entity.Repository.Base
{
    public interface IBaseRepository
    {
        IEntityContext Context { get; }

        Boolean Any();

        int Count();

        int SaveChanges();
    }

    public interface IBaseRepository<TKey, TEntity> : IBaseRepository
        where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        TEntity FindByKey(TKey key);

        void Add(params TEntity[] entities);

        void Add(IEnumerable<TEntity> entities);

        void Remove(params TEntity[] entities);

        void Remove(IEnumerable<TEntity> entities);

        void Update(params TEntity[] entities);

        void Update(IEnumerable<TEntity> entities);
    }
}
