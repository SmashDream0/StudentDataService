using StudentDataService.Entity.Specification.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Entity.Specification.Student
{
    public class ByGroupName : Specification<POCO.StudentEntity>
    {
        public ByGroupName(string groupName) : base(x => x.StudentToGroups.Any(g => g.Group.Name == groupName))
        { }
    }
}
