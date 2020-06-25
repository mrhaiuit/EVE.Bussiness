using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public class SubPrincipalCriteriaBE : BaseBE<SubPrincipalCriteria>, ISubPrincipalCriteriaBE
    {
        public SubPrincipalCriteriaBE(IUnitOfWork<EVEEntities> uoW) : base(uoW)
        {
        }

        public async Task<SubPrincipalCriteria> GetById(SubPrincipalCriteriaBaseReq req)
        {
            var obj = await GetAsync(c => c.EvalCriteriaId == req.EvalCriteriaId && c.EvalPeriodId == req.EvalPeriodId && c.SubPrincipalId == req.SubPrincipalId);
            if (obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

        public async Task<List<SubPrincipalCriteria>> GetByEmployeeAndPeriod(GetByEmployeeAndPeriodReq req)
        {
            var obj = await GetAsync(c => c.EvalPeriodId == req.EvalPeriodId && c.SubPrincipalId == req.SubPrincipalId);
            if (obj != null
               && obj.Any())
            {
                return obj.ToList();
            }

            return null;
        }

    }
}
