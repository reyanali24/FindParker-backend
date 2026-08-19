using ClassLibraryModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryDAL.Interfaces
{
    public interface ILoginHistoryInterface
    {
        int Create(LoginHistoryModel ob);

        List<LoginHistoryModel> Read();

        List<LoginHistoryModel> GetByUserId(long userId);

        int Update(LoginHistoryModel ob);

        int Delete(long historyId);
    }
}
