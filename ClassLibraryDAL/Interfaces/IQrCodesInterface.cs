using ClassLibraryModels;
using System;
using System.Collections.Generic;
using System.Text;


namespace ClassLibraryDAL.Interfaces
{
    public interface IQrCodesInterface
    {
        int Create(QrCodesModel ob);

        List<QrCodesModel> Read();

        int Update(QrCodesModel ob);

        int Delete(long qrId);

        PublicQrCodeModel? GetPublicQrCode(string qrCodeValue);
        List<QrCodesModel> GetByVehicleId(long vehicleId);
    }
}
