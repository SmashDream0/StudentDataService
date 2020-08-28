using System;
using System.Linq.Expressions;
using StudentDataService.Entity.Specification.Base.Expressions;

namespace StudentDataService.Entity.Specification.Base
{
    /// <summary>
    ///     Инверсия спецификации
    /// </summary>
    /// <typeparam name="T">Тип объекта, для которого применяется спецификация</typeparam>
    internal class NotSpecification<T> : Specification<T>
    {
        /// <summary>
        ///     Конструктор
        /// </summary>
        /// <param name="specification">Спецификация</param>
        /// <exception cref="ArgumentNullException" />
        public NotSpecification(Specification<T> specification)
            : base(specification)
        {
        }

        public override Expression<Func<T, bool>> Predicate
        {
            get
            {
                return base.Predicate.Not();
            }
        }
    }
}