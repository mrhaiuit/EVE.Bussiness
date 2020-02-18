using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public class EvalStandardBE : BaseBE<EvalStandard>, IEvalStandardBE
    {
        private IEvalCriteriaBE EvalCriteriaBE { get; set; }
        public EvalStandardBE(IUnitOfWork<EVEEntities> uoW, IEvalCriteriaBE evalCriteriaBE) : base(uoW)
        {
            EvalCriteriaBE = evalCriteriaBE;
        }
        public async Task<EvalStandard> GetById(EvalStandardBaseReq req)
        {
            var obj = await GetAsync(c => c.EvalStandardId == req.EvalStandardId);
            if (obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

        public async Task<List<EvalCriteria>> GetEvalCriteriaByStandard(int standardId)
        {
            var obj = await EvalCriteriaBE.GetAsync(p => p.EvalStandardId == standardId);
            return obj?.ToList();
        } 

    }
}
