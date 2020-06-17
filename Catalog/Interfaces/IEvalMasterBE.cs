using System.Collections.Generic;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.ApiModels.Catalog.Response;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IEvalMasterBE : IBaseBE<EvalMaster>
    {
        Task<EvalResultSumaryRes> GetEvalResultSumary(ExeEvalDetailByMasterIdReq req);
        Task<EvalMaster> GetByPeriodAndEmployee(EvalMasterGetByPeriodAndEmployeeReq req);
        Task<List<EvalDetail>> GetEvalDetailByMasterId(EvalMasterBaseReq req);
        Task<EvalMaster> GetById(EvalMasterBaseReq req);
        Task<List<EvalDetail>> ExeEvalDetailByMasterId(ExeEvalDetailByMasterIdReq req);
        Task<bool> CompleteFinal(EvalMasterBaseReq req);
        Task<bool> CancelFinal(EvalMasterBaseReq req);

        Task<List<EvalMasterGetByUserIdRes>> GetSelfEvalByUserId(EvalMasterGetByUserIdReq req);

        Task<List<EvalMasterGetByUserIdRes>> GetEvalByUserId(EvalMasterGetByUserIdReq req);

        Task<bool> IsAllowEdit(EvalMasterBaseReq req);
    }
}
