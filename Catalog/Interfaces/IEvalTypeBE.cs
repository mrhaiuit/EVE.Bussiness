using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IEvalTypeBE : IBaseBE<EvalType>
    {

        Task<EvalType> GetById(EvalTypeBaseReq req);
    }
}
