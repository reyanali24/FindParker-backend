using ClassLibraryModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryDAL.Interfaces
{
    public interface IAlertsInterface
    {
        int Create(AlertsModel ob);

        List<AlertsModel> Read();

        List<AlertsModel> GetByUserId(long userId);

        int Update(AlertsModel ob);

        int Delete(long alertId);
    }

}
