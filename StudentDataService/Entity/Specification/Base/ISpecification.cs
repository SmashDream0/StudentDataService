using System;
using System.Linq.Expressions;

namespace StudentDataService.Entity.Specification.Base
{
    /// <summary>
    ///     Интерфейс спецификации
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISpecification<T>
    {
        /// <summary>
        ///     Удовлетворяет ли объект спецификации
        /// </summary>
        /// <param name="item">Проверяемый объект</param>
        bool IsSatisfiedBy(T item);
        /// <summary>
        ///     Предикат для проверки
        /// </summary>
        Expression<Func<T, bool>> Predicate { get; }
    }
}