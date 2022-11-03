using LocalPrep.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalPrep.Services
{
   public interface IUserService
    {
        UserModel CreateUser(UserEntity userEntity);
        UserEntity GetValidUser(string UserName, string Password);

        UserModel UpdateUser(UserEntity userEntity);
       // IEnumerable<UserEntity> UserList();
    }
}
