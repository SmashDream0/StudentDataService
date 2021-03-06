﻿using StudentDataService.Entity.Specification.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Entity.Specification.Student
{
    public class BySurname : Specification<POCO.StudentEntity>
    {
        public BySurname(string surname) : base(x => x.Surname == surname)
        { }
    }
}
