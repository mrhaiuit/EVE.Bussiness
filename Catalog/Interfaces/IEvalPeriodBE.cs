using System.Collections.Generic;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.ApiModels.Catalog.Response;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IEvalPeriodBE : IBaseBE<EvalPeriod>
    {
        Task<List<EvalPeriodViewRes>> GetByYearAndSchool(EvalPeriodGetByYearAndSchoolReq req);
        Task<EvalPeriod> GetById(EvalPeriodBaseReq req);
    }
}
