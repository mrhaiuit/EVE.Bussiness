using System;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public class EvalStateBE : BaseBE<EvalState>, IEvalStateBE
    {
        public EvalStateBE(IUnitOfWork<EVEEntities> uoW) : base(uoW)
        {
        }
        public async Task<EvalState> GetById(EvalStateBaseReq req)
        {
            var obj = await GetAsync(c => c.EvalStateCode == req.EvalStateCode);
            if(obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

    }
}
