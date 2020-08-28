using StudentDataService.Entity.Repository.Base;
using StudentDataService.Entity.Repository.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Entity.Repository.User
{
    public interface IUserRepository : IBaseRepository<Int32, POCO.UserEntity>
    {
        POCO.UserEntity FindByLogin(string login);
    }
}
