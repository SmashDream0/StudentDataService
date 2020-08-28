using StudentDataService.Entity.Specification.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Entity.Specification.Group
{
    public class ByKey : Specification<POCO.GroupEntity>
    {
        public ByKey(Int32 key) : base(x => x.Key == key)
        { }
    }
}
