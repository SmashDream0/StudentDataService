using System;
using System.Linq.Expressions;

namespace StudentDataService.Entity.Specification.Base
{
    /// <summary>
    ///     Спецификация, которая всегда возвращает истину
    /// </summary>
    public sealed class TrueSpecification<T> : Specification<T>
    {
        public TrueSpecification() : base(element => true)
        {
        }
    }
}