using System.Collections.Generic;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IEvalStandardBE : IBaseBE<EvalStandard>
    {
        Task<List<EvalCriteria>> GetEvalCriteriaByStandard(int standardId);
        Task<EvalStandard> GetById(EvalStandardBaseReq req);
        Task<List<EvalStandard>> GetByLevelAndType(EvalStandardGetByLevelAndTypeReq req);
    }
}
