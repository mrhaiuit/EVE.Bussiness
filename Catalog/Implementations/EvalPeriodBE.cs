using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.ApiModels.Catalog.Response;
using EVE.Data;

namespace EVE.Bussiness
{
    public class EvalPeriodBE : BaseBE<EvalPeriod>, IEvalPeriodBE
    {
        public EvalPeriodBE(IUnitOfWork<EVEEntities> uoW) : base(uoW)
        {
        }
        public async Task<EvalPeriod> GetById(EvalPeriodBaseReq req)
        {
            var obj = await GetAsync(c => c.EvalPeriodId == req.EvalPeriodId);
            if(obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

        public async Task<List<EvalPeriodViewRes>> GetByYearAndSchool(EvalPeriodGetByYearAndSchoolReq req)
        {
            //var obj = await GetAsync(c => c.EvalPeriodId == req.EvalPeriodId);
            //if (obj != null
            //   && obj.Any())
            //{
            //    return obj.FirstOrDefault();
            //}

            return new List<EvalPeriodViewRes>();
        }
        
    }
}
