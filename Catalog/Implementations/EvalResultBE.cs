using System;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public class EvalResultBE : BaseBE<EvalResult>, IEvalResultBE
    {
        public EvalResultBE(IUnitOfWork<EVEEntities> uoW) : base(uoW)
        {
        }
        public async Task<EvalResult> GetById(EvalResultBaseReq req)
        {
            var obj = await GetAsync(c => c.EvalResultCode == req.EvalResultCode);
            if(obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

    }
}
