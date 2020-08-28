using StudentDataService.Entity.Specification.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Entity.Specification.StudentToGroup
{
    public class ByKey : Specification<POCO.StudentToGroupEntity>
    {
        public ByKey(Int32 key) : base(x => x.Key == key)
        { }
    }
}
