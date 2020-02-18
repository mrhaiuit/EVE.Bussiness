using System.Collections.Generic;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IEvalMasterBE : IBaseBE<EvalMaster>
    {
        Task<List<EvalDetail>> GetEvalDetailByMasterId(int MassterId);
        Task<EvalMaster> GetById(EvalMasterBaseReq req);
    }
}
