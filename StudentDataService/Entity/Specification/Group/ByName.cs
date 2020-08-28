using StudentDataService.Entity.Specification.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Entity.Specification.Group
{
    public class ByName : Specification<POCO.GroupEntity>
    {
        public ByName(string name) : base(x => x.Name == name)
        { }
    }
}
