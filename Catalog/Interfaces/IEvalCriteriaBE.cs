using System.Collections.Generic;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IEvalCriteriaBE : IBaseBE<EvalCriteria>
    {
        Task<List<EvalCriteria>> GetBySchoolLevel(GetByEvalTypeSchoolLevelReq req);
        Task<EvalGuide> GetGuideOfCriteria(GetGuideOfCriteriaReq req);
        Task<EvalCriteria> GetById(EvalCriteriaBaseReq req);
        Task<List<EvalCriteria>> GetByStandardId(EvalStandardBaseReq req);
    }
}
