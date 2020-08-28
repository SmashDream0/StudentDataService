using StudentDataService.Entity.Context;
using StudentDataService.Entity.Repository.Info;
using StudentDataService.Entity.POCO;
using StudentDataService.Entity.Specification.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Entity.Repository.Group
{
    public class GroupRepository : Base.BaseRepository<Int32, POCO.Group>, IGroupRepository
    {
        public GroupRepository(IEntityContext context) : base(context)
        { }

        /// <summary>
        /// Получить информацию по группе
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<GroupInfo> Find(string name)
        {
            return Find(new ByName(name))
                        .Select(x => new GroupInfo()
                        {
                            Key = x.Key,
                            Name = x.Name,
                            StudentCount = x.Students.Count()
                        });
        }
        public override POCO.Group FindByKey(int key)
        { return SingleOrDefault(new ByKey(key)); }
    }
}
