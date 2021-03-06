﻿using StudentDataService.Entity.Context;
using StudentDataService.Entity.Repository.Info;
using StudentDataService.Entity.POCO;
using StudentDataService.Entity.Specification.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Entity.Repository.User
{
    public class UserRepository : Base.BaseRepository<Int32, POCO.UserEntity>, IUserRepository
    {
        public UserRepository(IEntityContext context) : base(context)
        { }

        /// <summary>
        /// Получить информацию по группе
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public POCO.UserEntity FindByLogin(string login)
        { return SingleOrDefault(new ByLogin(login)); }

        public override POCO.UserEntity FindByKey(int key)
        { return SingleOrDefault(new ByKey(key)); }
    }
}
