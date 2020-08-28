using StudentDataService.Entity.Repository.Base;
using StudentDataService.Entity.Repository.Filter;
using StudentDataService.Entity.Repository.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Entity.Repository.Student
{
    public interface IStudentToGroupRepository: IBaseRepository<Int32, POCO.StudentToGroup>
    { }
}
