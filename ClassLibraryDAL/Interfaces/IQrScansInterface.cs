using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryDAL.Interfaces
{
    using ClassLibraryModels;

    namespace ClassLibraryDAL.Interfaces
    {
        public interface IQrScansInterface
        {
            int Create(QrScansModel ob);

            List<QrScansModel> Read();

            int Update(QrScansModel ob);

            int Delete(long scanId);
            List<QrScansModel> GetByVehicleId(long vehicleId);
        }
    }
}
