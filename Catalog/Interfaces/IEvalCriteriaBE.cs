using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IEvalCriteriaBE : IBaseBE<EvalCriteria>
    {
        Task<EvalGuide> GetGuideOfCriteria(GetGuideOfCriteriaReq req);
        Task<EvalCriteria> GetById(EvalCriteriaBaseReq req);
    }
}
