using StudentDataService.Entity.Specification.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Entity.Specification.User
{
    public class ByLogin : Specification<POCO.UserEntity>
    {
        public ByLogin(string login) : base(x => x.Login == login)
        { }
    }
}
