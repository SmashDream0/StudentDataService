using StudentDataService.Entity.POCO;
using StudentDataService.Entity.Repository.Filter;
using StudentDataService.Entity.Repository.Student;
using StudentDataService.Test.Fabric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace StudentDataService.Test.Entity.Repository.Student
{
    public class StudentRepositoryTest
    {
        /// <summary>
        /// Проверить пагинацию с первой страницей
        /// </summary>
        [Fact]
        public void FilterPaginationPageFirst()
        {
            var context = new MockFactory().GetContextSeparate();

            var target = new StudentEntity() { Key = 1 };

            context.Set<StudentEntity>().AddRange(new[] { target, new StudentEntity() { Key = 2 } });
            context.SaveChanges();

            var repository = new StudentRepository(context);

            var result = repository.Find(new StudentFilter() { Pagination = new PaginationFilter() { Index = 0, Size = 1 } });

            Assert.Equal(1, result.First().Key);
        }

        /// <summary>
        /// Проверить пагинацию со второй страницей
        /// </summary>
        [Fact]
        public void FilterPaginationPageSecond()
        {
            var context = new MockFactory().GetContextSeparate();

            var target = new StudentEntity() { Key = 1 };

            context.Set<StudentEntity>().AddRange(new[] { target, new StudentEntity() { Key = 2 } });
            context.SaveChanges();

            var repository = new StudentRepository(context);

            var result = repository.Find(new StudentFilter() { Pagination = new PaginationFilter() { Index = 1, Size = 1 } });

            Assert.Equal(2, result.First().Key);
        }

        /// <summary>
        /// Проверить пагинацию с размером страницы в одну позицию
        /// </summary>
        [Fact]
        public void FilterPaginationPageSizeOne()
        {
            var context = new MockFactory().GetContextSeparate();

            context.Set<StudentEntity>().AddRange(new[] { new StudentEntity(), new StudentEntity() });
            context.SaveChanges();

            var repository = new StudentRepository(context);

            var result = repository.Find(new StudentFilter() { Pagination = new PaginationFilter() { Index = 0, Size = 1 } });

            Assert.Single(result);
        }

        /// <summary>
        /// Проверить пагинацию с размером страницы в две позиции
        /// </summary>
        [Fact]
        public void FilterPaginationPageSizeTwo()
        {
            var context = new MockFactory().GetContextSeparate();

            context.Set<StudentEntity>().AddRange(new[] { new StudentEntity(), new StudentEntity()});
            context.SaveChanges();

            var repository = new StudentRepository(context);

            var result = repository.Find(new StudentFilter() { Pagination = new PaginationFilter() { Index = 0, Size = 2 } });

            Assert.Equal(2, result.Count());
        }

        /// <summary>
        /// Проверить поиск по полу
        /// </summary>
        [Fact]
        public void FilterBySex()
        {
            var context = new MockFactory().GetContextSeparate();

            var target = new StudentEntity() { Sex = ESex.Female };
            context.Set<StudentEntity>().AddRange(new[] { target, new StudentEntity() { Sex = ESex.Male }, new StudentEntity() { Sex = ESex.Male } });
            context.SaveChanges();

            var repository = new StudentRepository(context);

            var result = repository.Find(new StudentFilter() { Sex = ESex.Female });

            Assert.Single(result);
            Assert.Equal(target.Key, result.First().Key);
        }

        /// <summary>
        /// Проверить поиск по имени
        /// </summary>
        [Fact]
        public void FilterByFirstname()
        {
            var context = new MockFactory().GetContextSeparate();

            var target = new StudentEntity() { Firstname = "1" };
            context.Set<StudentEntity>().AddRange(new[] { target, new StudentEntity() { Firstname = "2" }, new StudentEntity() { Firstname = "2" } });
            context.SaveChanges();

            var repository = new StudentRepository(context);

            var result = repository.Find(new StudentFilter() { Firstname = "1" });

            Assert.Single(result);
            Assert.Equal(target.Key, result.First().Key);
        }

        /// <summary>
        /// Проверить поиск по фамилии
        /// </summary>
        [Fact]
        public void FilterBySurname()
        {
            var context = new MockFactory().GetContextSeparate();

            var target = new StudentEntity() { Surname = "1" };
            context.Set<StudentEntity>().AddRange(new[] { target, new StudentEntity() { Surname = "2" }, new StudentEntity() { Surname = "2" } });
            context.SaveChanges();

            var repository = new StudentRepository(context);

            var result = repository.Find(new StudentFilter() { Surname = "1" });

            Assert.Single(result);
            Assert.Equal(target.Key, result.First().Key);
        }

        /// <summary>
        /// Проверить поиск по отчеству
        /// </summary>
        [Fact]
        public void FilterByMiddlename()
        {
            var context = new MockFactory().GetContextSeparate();

            var target = new StudentEntity() { Middlename = "1" };
            context.Set<StudentEntity>().AddRange(new[] { target, new StudentEntity() { Middlename = "2" }, new StudentEntity() { Middlename = "2" } });
            context.SaveChanges();

            var repository = new StudentRepository(context);

            var result = repository.Find(new StudentFilter() { Middlename = "1" });

            Assert.Single(result);
            Assert.Equal(target.Key, result.First().Key);
        }

        /// <summary>
        /// Проверить поиск по имени группы
        /// </summary>
        [Fact]
        public void FilterByGroupName()
        {
            var context = new MockFactory().GetContextSeparate();

            var target = new StudentEntity() { Key = 1 };

            context.Set<StudentEntity>().AddRange(new[] { target, new StudentEntity() { Key = 2 }, new StudentEntity() { Key = 3 } });

            context.Set<GroupEntity>().AddRange(new[] { new GroupEntity() { Key = 1, Name = "1" }, new GroupEntity() { Key = 2, Name = "2" } });

            context.Set<StudentToGroupEntity>().AddRange(new[]
            {
                new StudentToGroupEntity() { GroupKey = 1, StudentKey = 1 },
                new StudentToGroupEntity() { GroupKey = 2, StudentKey = 2 }, 
                new StudentToGroupEntity() { GroupKey = 2, StudentKey = 3 } 
            });

            context.SaveChanges();

            var repository = new StudentRepository(context);

            var result = repository.Find(new StudentFilter() { GroupName = "1" });

            Assert.Single(result);
            Assert.Equal(target.Key, result.First().Key);
        }

        /// <summary>
        /// Проверить коллекцию имен групп в выдаче по фильтру
        /// </summary>
        [Fact]
        public void FilterCheckGroupNames()
        {
            var context = new MockFactory().GetContextSeparate();

            var target = new StudentEntity() { Key = 1 };

            context.Set<StudentEntity>().AddRange(new[] { target, new StudentEntity() { Key = 2 }, new StudentEntity() { Key = 3 } });

            context.Set<GroupEntity>().AddRange(new[] { new GroupEntity() { Key = 1, Name = "1" }, new GroupEntity() { Key = 2, Name = "2" } });

            context.Set<StudentToGroupEntity>().AddRange(new[]
            {
                new StudentToGroupEntity() { GroupKey = 1, StudentKey = 1 },
                new StudentToGroupEntity() { GroupKey = 2, StudentKey = 1 },
                new StudentToGroupEntity() { GroupKey = 2, StudentKey = 3 }
            });

            context.SaveChanges();

            var repository = new StudentRepository(context);

            var result = repository.Find(new StudentFilter() { GroupName = "2" });

            Assert.Equal(new[] { "1", "2" }, result.First().GroupNames);
        }
    }
}
