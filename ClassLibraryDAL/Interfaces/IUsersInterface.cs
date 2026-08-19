using ClassLibraryModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryDAL.Interfaces
{
    public interface IUsersInterface
    {
        long CreateUser(UsersModel ob);
        List<UsersModel> Read();
        int Update(UsersModel ob);
        int Delete(long userId);

    }
}
