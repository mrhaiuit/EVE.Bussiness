using System;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public class EvalTypeBE : BaseBE<EvalType>, IEvalTypeBE
    {
        public EvalTypeBE(IUnitOfWork<EVEEntities> uoW) : base(uoW)
        {
        }
        public async Task<EvalType> GetById(EvalTypeBaseReq req)
        {
            var obj = await GetAsync(c => c.EvalTypeCode == req.EvalTypeCode);
            if(obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

    }
}
