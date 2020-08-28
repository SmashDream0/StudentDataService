using StudentDataService.Entity.Context;
using StudentDataService.Entity.Specification.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Entity.Repository.Base
{
    public abstract class BaseRepository<TKey, TEntity> : IBaseRepository<TKey, TEntity>
        where TEntity : class
    {
        public BaseRepository(IEntityContext context)
        { Context = context; }

        /// <summary>
        /// Контекст
        /// </summary>
        public IEntityContext Context
        { get; private set; }

        /// <summary>
        /// Существуют ли записи
        /// </summary>
        /// <returns></returns>
        public Boolean Any()
        { return Context.Set<TEntity>().Any(); }

        /// <summary>
        /// Получить количество записей
        /// </summary>
        /// <returns></returns>
        public int Count()
        { return Context.Set<TEntity>().Count(); }

        /// <summary>
        /// Добавить элементы
        /// </summary>
        /// <param name="entities"></param>
        public void Add(params TEntity[] entities)
        { Context.AddRange(entities); }

        /// <summary>
        /// Добавить элементы
        /// </summary>
        /// <param name="entities"></param>
        public void Add(IEnumerable<TEntity> entities)
        { Context.AddRange(entities); }

        /// <summary>
        /// Удалить элементы
        /// </summary>
        /// <param name="entities"></param>
        public void Remove(params TEntity[] entities)
        {
            Context.AttachRange(entities);
            Context.RemoveRange(entities);
        }

        /// <summary>
        /// Удалить элементы
        /// </summary>
        /// <param name="entities"></param>
        public void Remove(IEnumerable<TEntity> entities)
        {
            Context.AttachRange(entities);
            Context.RemoveRange(entities);
        }

        /// <summary>
        /// Обновить сущности
        /// </summary>
        /// <param name="entities"></param>
        public void Update(params TEntity[] entities)
        {
            Context.AttachRange(entities);
            Context.UpdateRange(entities);
        }

        /// <summary>
        /// Обновить сущности
        /// </summary>
        public void Update(IEnumerable<TEntity> entities)
        {
            Context.AttachRange(entities);
            Context.UpdateRange(entities);
        }

        /// <summary>
        /// Найти все записи таблицы
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAll()
        { return Context.Set<TEntity>().ToArray(); }

        /// <summary>
        /// Получить сущность по первичному ключу
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public abstract TEntity FindByKey(TKey key);

        /// <summary>
        /// Получить запрос по критерию
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        protected IEnumerable<TEntity> Find(ISpecification<TEntity> specification)
        {
            var set = Context.Set<TEntity>().Where(specification.Predicate);

            return set.ToArray();
        }

        /// <summary>
        /// Получить кол-во элементов по критерию
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        protected int Count(ISpecification<TEntity> specification)
        { return Context.Set<TEntity>().Count(specification.Predicate); }

        /// <summary>
        /// Получить один элемент по критерию, либо значение по умолчанию
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        protected TEntity SingleOrDefault(ISpecification<TEntity> specification)
        { return Context.Set<TEntity>().SingleOrDefault(specification.Predicate); }

        /// <summary>
        /// Получить один элемент по критерию
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        protected TEntity Single(ISpecification<TEntity> specification)
        { return Context.Set<TEntity>().Single(specification.Predicate); }

        /// <summary>
        /// Проурить существование элементов по критерию
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        protected Boolean Any(ISpecification<TEntity> specification)
        { return Context.Set<TEntity>().Any(specification.Predicate); }

        /// <summary>
        /// Сохранить изменения
        /// </summary>
        public int SaveChanges()
        { return Context.SaveChanges(); }
    }
}