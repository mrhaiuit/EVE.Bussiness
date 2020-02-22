using System.Collections.Generic;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.ApiModels.Catalog.Response;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IEvalPeriodBE : IBaseBE<EvalPeriod>
    {
        List<usp_GetPeriodByYearAndSchool_Result> GetByYearAndSchool(EvalPeriodGetByYearAndSchoolReq req);
        Task<EvalPeriod> GetById(EvalPeriodBaseReq req);
    }
}
