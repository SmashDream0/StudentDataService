using System;
using System.Linq;
using System.Linq.Expressions;
using StudentDataService.Entity.Specification.Base.Expressions;

namespace StudentDataService.Entity.Specification.Base
{
    /// <summary>
    ///     Объединение спецификаций по ИЛИ
    /// </summary>
    /// <typeparam name="T">Тип объекта, для которого применяется спецификация</typeparam>
    internal class OrSpecification<T> : CompositeSpecification<T>
    {
        /// <summary>
        ///     Конструктор
        /// </summary>
        /// <param name="specifications">Список спецификаций</param>
        public OrSpecification(params Specification<T>[] specifications)
            : base(specifications)
        {
        }

        public override Expression<Func<T, bool>> Predicate
        {
            get
            {
                Expression<Func<T, bool>> result = Specifications.First();
                return Specifications.Skip(1).Aggregate(result, (current, specification) => current.Or(specification));
            }
        }
    }
}