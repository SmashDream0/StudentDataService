using StudentDataService.Entity.POCO;
using StudentDataService.Entity.Specification.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Entity.Specification.Student
{
    public class BySex : Specification<POCO.Student>
    {
        public BySex(ESex sex) : base(x => x.Sex == sex)
        { }
    }
}
