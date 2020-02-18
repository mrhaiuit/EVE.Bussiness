using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IEvalPeriodBE : IBaseBE<EvalPeriod>
    {

        Task<EvalPeriod> GetById(EvalPeriodBaseReq req);
    }
}
