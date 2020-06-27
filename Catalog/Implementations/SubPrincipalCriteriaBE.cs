using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.ApiModels.Catalog.Response;
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

        public async Task<List<SubPrincipalEmployeeAndPeriodRes>> GetByEmployeeAndPeriod(GetByEmployeeAndPeriodReq req)
        {
            var obj = from s in _uoW.Context.SubPrincipalCriterias
                      join dt in _uoW.Context.EvalCriterias on s.EvalCriteriaId equals dt.EvalCriteriaId
                      join mt in _uoW.Context.EvalStandards on dt.EvalStandardId equals mt.EvalStandardId
                      where s.EvalPeriodId == req.EvalPeriodId && s.SubPrincipalId == req.SubPrincipalId
                      select new SubPrincipalEmployeeAndPeriodRes
                      {
                          EvalCriteriaId = s.EvalCriteriaId,
                          EvalPeriodId = s.EvalPeriodId,
                          EvalCriteriaName = dt.EvalCriteriaName,
                          EvalStandardName = mt.EvalStandardName,
                          Idx = dt.Idx ?? 0,
                          SubPrincipalId = s.SubPrincipalId
                      };

               // await GetAsync(c => c.EvalPeriodId == req.EvalPeriodId && c.SubPrincipalId == req.SubPrincipalId);
            if (obj != null
               && obj.Any())
            {
                return obj.ToList();
            }

            return null;
        }

    }
}
