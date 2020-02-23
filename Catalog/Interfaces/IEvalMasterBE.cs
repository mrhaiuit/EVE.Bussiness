using System.Collections.Generic;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IEvalMasterBE : IBaseBE<EvalMaster>
    {
        Task<EvalMaster> GetByPeriodAndEmployee(EvalMasterGetByPeriodAndEmployeeReq req);
        Task<List<EvalDetail>> GetEvalDetailByMasterId(EvalMasterBaseReq req);
        Task<EvalMaster> GetById(EvalMasterBaseReq req);
        Task<List<EvalDetail>> ExeEvalDetailByMasterId(ExeEvalDetailByMasterIdReq req);
    }
}
