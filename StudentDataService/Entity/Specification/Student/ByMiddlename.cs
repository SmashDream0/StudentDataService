using StudentDataService.Entity.Specification.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Entity.Specification.Student
{
    public class ByMiddlename : Specification<POCO.Student>
    {
        public ByMiddlename(string middlename) : base(x => x.Middlename == middlename)
        { }
    }
}
