using ClassLibraryModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryDAL.Interfaces
{
    public interface IUserProfilesInterface
    {
        int Create(UserProfilesModel ob);
        List<UserProfilesModel> Read();
        int Update(UserProfilesModel ob);
        int Delete(long profileId);
        List<UserProfilesModel> GetByUserId(long userId);
    }
}
