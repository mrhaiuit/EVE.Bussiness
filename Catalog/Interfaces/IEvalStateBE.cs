using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IEvalStateBE : IBaseBE<EvalState>
    {

        Task<EvalState> GetById(EvalStateBaseReq req);
    }
}
