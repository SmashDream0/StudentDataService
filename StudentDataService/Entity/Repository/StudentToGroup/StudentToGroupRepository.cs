using StudentDataService.Entity.Context;
using StudentDataService.Entity.Repository.Info;
using StudentDataService.Entity.POCO;
using StudentDataService.Entity.Specification.Base;
using StudentDataService.Entity.Specification.StudentToGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentDataService.Entity.Repository.Filter;
using Microsoft.EntityFrameworkCore;

namespace StudentDataService.Entity.Repository.Student
{
    public class StudentToGroupRepository : Base.BaseRepository<Int32, POCO.StudentToGroupEntity>, IStudentToGroupRepository
    {
        public StudentToGroupRepository(IEntityContext context) : base(context)
        { }

        public override POCO.StudentToGroupEntity FindByKey(int key)
        { return SingleOrDefault(new ByKey(key)); }
    }
}