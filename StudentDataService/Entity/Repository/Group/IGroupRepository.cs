using StudentDataService.Entity.Repository.Base;
using StudentDataService.Entity.Repository.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Entity.Repository.Group
{
    public interface IGroupRepository : IBaseRepository<Int32, POCO.Group>
    {
        IEnumerable<GroupInfo> Find(string name);
    }
}
