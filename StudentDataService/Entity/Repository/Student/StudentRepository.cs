using StudentDataService.Entity.Context;
using StudentDataService.Entity.Repository.Info;
using StudentDataService.Entity.POCO;
using StudentDataService.Entity.Specification.Base;
using StudentDataService.Entity.Specification.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentDataService.Entity.Repository.Filter;
using Microsoft.EntityFrameworkCore;

namespace StudentDataService.Entity.Repository.Student
{
    public class StudentRepository : Base.BaseRepository<Int32, POCO.StudentEntity>, IStudentRepository
    {
        public StudentRepository(IEntityContext context) : base(context)
        { }

        /// <summary>
        /// Получить список студентов по фильтрам
        /// </summary>
        /// <param name="filter">Фильтры</param>
        /// <returns></returns>
        public IEnumerable<StudentInfo> Find(StudentFilter filter)
        {
            ISpecification<POCO.StudentEntity> specification = null;

            if (!String.IsNullOrEmpty(filter.Firstname))
            { specification ??= new ByFirstname(filter.Firstname); }

            if (!String.IsNullOrEmpty(filter.GroupName))
            { specification ??= new ByGroupName(filter.GroupName); }

            if (!String.IsNullOrEmpty(filter.Middlename))
            { specification ??= new ByMiddlename(filter.Middlename); }

            if (!String.IsNullOrEmpty(filter.Surname))
            { specification ??= new BySurname(filter.Surname); }

            if (filter.Sex.HasValue)
            { specification ??= new BySex(filter.Sex.Value); }

            var query = Context.Set<POCO.StudentEntity>().Include(x => x.StudentToGroups).Where(x => true);

            if (specification != null)
            { query = query.Where(specification.Predicate); }

            if (filter.Pagination != null)
            {
                if (filter.Pagination.Size < 1)
                { throw new ArgumentOutOfRangeException($"Page size({filter.Pagination.Size}) less then 1"); }
                if (filter.Pagination.Index < 0)
                { throw new ArgumentOutOfRangeException($"Page index({filter.Pagination.Index}) less then 0"); }

                query = query.Skip(filter.Pagination.Index * filter.Pagination.Size)
                             .Take(filter.Pagination.Size);
            }

            return query.Select(x => new StudentInfo()
            {
                Key = x.Key,
                Code = x.Code,
                Firstname = x.Firstname,
                Middlename = x.Middlename,
                Sex = x.Sex,
                Surname = x.Surname,
                GroupNames = x.StudentToGroups.Select(x => x.Group.Name).ToArray()
            }).ToArray();
        }

        public override POCO.StudentEntity FindByKey(int key)
        { return SingleOrDefault(new ByKey(key)); }
    }
}