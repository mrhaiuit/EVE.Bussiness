using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IEvalResultBE : IBaseBE<EvalResult>
    {

        Task<EvalResult> GetById(EvalResultBaseReq req);
    }
}
