using ClassLibraryModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryDAL.Interfaces
{
    public interface IEmergencyContactsInterface
    {
        int Create(EmergencyContactsModel ob);

        List<EmergencyContactsModel> Read();

        int Update(EmergencyContactsModel ob);

        int Delete(long contactId);

        List<EmergencyContactsModel> GetByUserId(long userId);
    }
}
