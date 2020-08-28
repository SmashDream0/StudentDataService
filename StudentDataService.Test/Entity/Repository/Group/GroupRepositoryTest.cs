using StudentDataService.Entity.POCO;
using StudentDataService.Entity.Repository.Group;
using StudentDataService.Test.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace StudentDataService.Test.Entity.Repository.Group
{
    public class GroupRepositoryTest
    {
        [Fact]
        public void FindByNameExist()
        {
            var context = new MockFactory().GetContextSeparate();

            var target = new GroupEntity() { Name = "12" };
            context.Set<GroupEntity>().AddRange(new[] { target, new GroupEntity() { Name = "123" } });
            context.SaveChanges();

            var repository = new GroupRepository(context);

            var result = repository.Find("12");

            Assert.Equal(target.Key, result.First().Key);
        }

        [Fact]
        public void FindByNameNotExist()
        {
            var context = new MockFactory().GetContextSeparate();

            var target = new GroupEntity() { Name = "12" };
            context.Set<GroupEntity>().AddRange(new[] { target, new GroupEntity() { Name = "123" } });
            context.SaveChanges();

            var repository = new GroupRepository(context);

            var result = repository.Find("1");

            Assert.Empty(result);
        }
    }
}
