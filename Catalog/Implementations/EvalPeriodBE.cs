using System;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
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

    }
}
