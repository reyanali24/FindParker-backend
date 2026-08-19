using ClassLibraryModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryDAL.Interfaces
{
    public interface IPrivacySettingsInterface
    {
        int Create(PrivacySettingsModel ob);
        List<PrivacySettingsModel> Read();
        int Update(PrivacySettingsModel ob);
        int Delete(long settingId);
        List<PrivacySettingsModel> GetByUserId(long userId);
    }
}
