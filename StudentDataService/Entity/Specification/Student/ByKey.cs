using StudentDataService.Entity.POCO;
using StudentDataService.Entity.Specification.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Entity.Specification.Student
{
    public class ByKey : Specification<POCO.StudentEntity>
    {
        public ByKey(Int32 key) : base(x => x.Key == key)
        { }
    }
}
