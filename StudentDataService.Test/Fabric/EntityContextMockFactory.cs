using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using StudentDataService.Entity.Context;

namespace StudentDataService.Test.Fabric
{
    public class EntityContextMockFactory
    {
        /// <summary>
        /// Очистить тестовую БД
        /// </summary>
        public void ClearData<TEntityContext>(String dbName)
            where TEntityContext : DbContext, IEntityContext
        {
            var context = Create<TEntityContext>(dbName);

            //зачищаю данные БД, т.к. данные в памяти статичны
            //в то же время создание экземпляра данных - InMemoryDatabaseRoot приводит к тормозам, при каждом создании контекста, через этот метод
            (context as DbContext).Database.EnsureDeleted();
        }

        public IEntityContext CreateWithCleanData<TEntityContext>(String dbName)
            where TEntityContext : DbContext, IEntityContext
        {
            var context = Create<TEntityContext>(dbName);

            //зачищаю данные БД, т.к. данные в памяти статичны
            //в то же время создание экземпляра данных - InMemoryDatabaseRoot приводит к тормозам, при каждом создании контекста, через этот метод
            (context as DbContext).Database.EnsureDeleted();

            return context;
        }

        /// <summary>
        /// Создать контекст с уникальным хранилище данных
        /// </summary>
        /// <typeparam name="TEntityContext"></typeparam>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public IEntityContext CreateWithSeparateData<TEntityContext>(String dbName)
            where TEntityContext : DbContext, IEntityContext
        {
            var options = new DbContextOptionsBuilder<TEntityContext>()
            .UseInMemoryDatabase(dbName, new InMemoryDatabaseRoot())
            .Options;

            var instance = Activator.CreateInstance(typeof(TEntityContext), options);

            var context = instance as DbContext;

            return context as TEntityContext;
        }

        /// <summary>
        /// Создать контекст с разделяемым хранилищем данных
        /// </summary>
        /// <typeparam name="TEntityContext"></typeparam>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public IEntityContext Create<TEntityContext>(String dbName)
            where TEntityContext : DbContext, IEntityContext
        {
            var options = new DbContextOptionsBuilder<TEntityContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

            var instance = Activator.CreateInstance(typeof(TEntityContext), options);

            var context = instance as DbContext;

            return context as TEntityContext;
        }
    }
}
