using ClassLibraryModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryDAL.Interfaces
{
    public interface IVehiclesInterface
    {
        int Create(VehiclesModel ob);

        List<VehiclesModel> Read();

        int Update(VehiclesModel ob);

        int Delete(long vehicleId);
        List<VehiclesModel> GetByUserId(long userId);
    }
}
