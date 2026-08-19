using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryModels
{
    public interface IMaskedCallsInterface
    {
        int Create(MaskedCallsModel ob);

        List<MaskedCallsModel> Read();

        int Update(MaskedCallsModel ob);

        int Delete(long callId);
        List<MaskedCallsModel> GetByUserId(long userId);
    }
}
