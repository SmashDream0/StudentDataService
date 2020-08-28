using System;
using System.Linq.Expressions;

namespace StudentDataService.Entity.Specification.Base
{
    /// <summary>
    ///     Базовый класс спецификации
    /// </summary>
    /// <typeparam name="T">Тип объекта, для которого применяется спецификация</typeparam>
    public abstract class Specification<T> : ISpecification<T>
    {
        /// <summary>
        ///     Предикат для проверки
        /// </summary>
        private Expression<Func<T, bool>> _predicate;
        /// <summary>
        ///     Предикат для проверки
        /// </summary>
        public virtual Expression<Func<T, bool>> Predicate
        {
            get { return _predicate; }
            protected set { _predicate = value; }
        }

        /// <summary>
        ///     Конструктор
        /// </summary>
        protected Specification()
        {
        }

        /// <summary>
        ///     Конструктор
        /// </summary>
        /// <param name="predicat">Предикат фильтрации</param>
        protected Specification(Expression<Func<T, bool>> predicat)
        {
            _predicate = predicat;
        }

        /// <summary>
        ///     Удовлетворяет ли объект спецификации
        /// </summary>
        /// <param name="item">Проверяемый объект</param>
        public bool IsSatisfiedBy(T item)
        {
            return Predicate.Compile()(item);
        }

        #region Операторы
        /// <summary>
        ///     Оператор НЕ
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public static Specification<T> operator !(Specification<T> specification)
        {
            return new NotSpecification<T>(specification);
        }

        /// <summary>
        ///     Оператор ИЛИ
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Specification<T> operator |(Specification<T> left, Specification<T> right)
        {
            return new OrSpecification<T>(left, right);
        }

        /// <summary>
        ///     Оператор И
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Specification<T> operator &(Specification<T> left, Specification<T> right)
        {
            return new AndSpecification<T>(left, right);
        }
        #endregion

        #region Приведение
        public static implicit operator Predicate<T>(Specification<T> specification)
        {
            return specification.IsSatisfiedBy;
        }

        public static implicit operator Func<T, bool>(Specification<T> specification)
        {
            return specification.IsSatisfiedBy;
        }

        public static implicit operator Expression<Func<T, bool>>(Specification<T> specification)
        {
            return specification.Predicate;
        }
        #endregion
    }
}