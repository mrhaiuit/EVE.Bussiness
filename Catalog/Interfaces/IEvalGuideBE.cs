using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IEvalGuideBE : IBaseBE<EvalGuide>
    {

        Task<EvalGuide> GetById(EvalGuideBaseReq req);
    }
}
