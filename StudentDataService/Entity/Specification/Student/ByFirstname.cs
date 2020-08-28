using StudentDataService.Entity.Specification.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Entity.Specification.Student
{
    public class ByFirstname : Specification<POCO.Student>
    {
        public ByFirstname(string firstname) : base(x => x.Firstname == firstname)
        { }
    }
}
