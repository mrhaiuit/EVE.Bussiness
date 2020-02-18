using System;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public class EvalDetailBE : BaseBE<EvalDetail>, IEvalDetailBE
    {
        public EvalDetailBE(IUnitOfWork<EVEEntities> uoW) : base(uoW)
        {
        }
        public async Task<EvalDetail> GetById(EvalDetailBaseReq req)
        {
            var obj = await GetAsync(c => c.EvalDetailId == req.EvalDetailId);
            if(obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

    }
}
