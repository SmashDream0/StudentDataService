using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentDataService.Attributes;
using StudentDataService.Contracts.Attributes;
using StudentDataService.Contracts.Request;
using StudentDataService.Contracts.Response;
using StudentDataService.Entity.Repository.Group;
using StudentDataService.Entity.Repository.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ValidateModel]
    public class GroupController : BaseCRUIDController<Int32, String, GroupSelect>
    {
        public GroupController(ILogger<GroupController> logger, IGroupRepository groupRepository, IStudentToGroupRepository studentToGroupRepository) : base(logger)
        {
            _groupRepository = groupRepository;
            _studentToGroupRepository = studentToGroupRepository;
        }

        private readonly IGroupRepository _groupRepository;
        private readonly IStudentToGroupRepository _studentToGroupRepository;

        [HttpGet(nameof(Get))]
        [Authorize(Roles = "admin,user")]
        public GroupsResponse Get([FromRoute] string name)
        {
            var result = _groupRepository.Find(name);

            return new GroupsResponse()
            {
                Groups = result.Select(x => new GroupResponse()
                {
                    Key = x.Key,
                    Name = x.Name,
                    StudentCount = x.StudentCount,
                }).ToArray(),
            };
        }

        [HttpPost(nameof(JoinGroupStudent))]
        [Authorize(Roles = "admin")]
        public void JoinGroupStudent([FromBody]JoinGroupStudent join)
        {
            _studentToGroupRepository.Add(new Entity.POCO.StudentToGroupEntity() { GroupKey = join.GroupKey, StudentKey = join.StudentKey });
            _studentToGroupRepository.SaveChanges();
        }

        public override int Insert([FromQuery] String name)
        {
            var group = new Entity.POCO.GroupEntity()
            { 
                Name = name,
            };

            _groupRepository.Add(group);
            _groupRepository.SaveChanges();

            return group.Key;
        }

        public override GroupSelect GetByKey(int key)
        {
            var group = _groupRepository.FindByKey(key);

            GroupSelect result = null;

            if (group != null)
            { result = new GroupSelect() { Key = group.Key, Name = group.Name }; }

            return result;
        }

        public override IEnumerable<GroupSelect>GetAll()
        {
            var groups = _groupRepository.GetAll();

            return groups.Select(x => new GroupSelect() { Key = x.Key, Name = x.Name }).ToArray();
        }

        public override bool Update(GroupSelect model)
        {
            var group = _groupRepository.FindByKey(model.Key);

            var result = (group != null);

            if (result)
            {
                if (!String.IsNullOrEmpty(model.Name))
                { group.Name = model.Name; }

                _groupRepository.Update(group);
                _groupRepository.SaveChanges();
            }

            return result;
        }

        public override void Remove(int key)
        {
            _groupRepository.Remove(new Entity.POCO.GroupEntity() { Key = key });
            _groupRepository.SaveChanges();
        }
    }
}