using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public class EvalCriteriaBE : BaseBE<EvalCriteria>, IEvalCriteriaBE
    {
        private readonly IEvalGuideBE EvalGuideBE;
        public EvalCriteriaBE(IUnitOfWork<EVEEntities> uoW,
            IEvalGuideBE evalGuideBE
            ) : base(uoW)
        {
            EvalGuideBE = evalGuideBE;
        } 
        public async Task<EvalCriteria> GetById(EvalCriteriaBaseReq req)
        {
            var obj = await GetAsync(c => c.EvalCriteriaId == req.EvalCriteriaId);
            if(obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

        public async Task<List<EvalCriteria>> GetByStandardId(EvalStandardBaseReq req)
        {
            var obj = await GetAsync(c => c.EvalStandardId == req.EvalStandardId);
            if (obj != null
               && obj.Any())
            {
                return obj.ToList();
            }

            return null;
        }

        public async Task<EvalGuide> GetGuideOfCriteria(GetGuideOfCriteriaReq req)
        {
            var obj = await EvalGuideBE.GetAsync(c => c.EvalCriteriaId == req.EvalCriteriaId && c.EvalResultCode == req.EvalResultCode);
            if (obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

    }
}
