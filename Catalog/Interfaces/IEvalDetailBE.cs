using System.Collections.Generic;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.ApiModels.Catalog.Response;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IEvalDetailBE : IBaseBE<EvalDetail>
    {
        Task<List<EvalDetailByYearAndUserRes>> GetByByYearAndUser(EvalDetailByYearAndUserReq req);
        Task<EvalDetail> GetById(EvalDetailBaseReq req);
        Task<string> GetGroupResultByYearAndUser(EvalDetailByYearAndUserReq req);
    }
}
