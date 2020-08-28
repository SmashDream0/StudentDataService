using System;
using System.Collections.Generic;
using System.Text;
using StudentDataService.Entity.Context;

namespace StudentDataService.Test.Factory
{
    public class MockFactory
    {
        /// <summary>
        /// Получить контекст с уникальным хранилищем данных
        /// </summary>
        /// <returns></returns>
        public IEntityContext GetContextSeparate()
        { return new EntityContextMockFactory().CreateWithSeparateData<EntityContext>("Test"); }

        /// <summary>
        /// Получить контекст с разделяемым хранилищем данных
        /// </summary>
        /// <returns></returns>
        public IEntityContext GetContext()
        { return new EntityContextMockFactory().Create<EntityContext>("Test"); }

        /// <summary>
        /// Очистить разделяемое хранилище данных
        /// </summary>
        public void Clear()
        { new EntityContextMockFactory().ClearData<EntityContext>("Test"); }
    }
}
