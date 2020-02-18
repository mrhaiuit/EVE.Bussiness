using System;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public class EvalGuideBE : BaseBE<EvalGuide>, IEvalGuideBE
    {
        public EvalGuideBE(IUnitOfWork<EVEEntities> uoW) : base(uoW)
        {
        }
        public async Task<EvalGuide> GetById(EvalGuideBaseReq req)
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
