using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IEvalDetailBE : IBaseBE<EvalDetail>
    {

        Task<EvalDetail> GetById(EvalDetailBaseReq req);
    }
}
