using System;
using System.Linq.Expressions;

namespace StudentDataService.Entity.Specification.Base
{
    /// <summary>
    ///     Спецификация, которая всегда возвращает ложь
    /// </summary>
    public sealed class FalseSpecification<T> : Specification<T>
    {
        public FalseSpecification()
            : base(element => false)
        {
            
        }
    }
}