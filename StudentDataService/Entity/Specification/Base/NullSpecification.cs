using System;
using System.Linq.Expressions;

namespace StudentDataService.Entity.Specification.Base
{
    /// <summary>
    ///     Пустая спецификация
    /// </summary>
    /// <typeparam name="T">Тип объекта, для которого применяется спецификация</typeparam>
    public class NullSpecification<T> : Specification<T>
    {
        public NullSpecification()
            : base(item => true)
        {
        }
    }
}