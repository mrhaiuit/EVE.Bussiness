using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public class EvalMasterBE : BaseBE<EvalMaster>, IEvalMasterBE
    {
        private IEvalDetailBE EvalDetailBE { get; set; }
        public EvalMasterBE(IUnitOfWork<EVEEntities> uoW,IEvalDetailBE evalDetailBE ) : base(uoW)
        {
            EvalDetailBE = evalDetailBE;
        }
        public async Task<EvalMaster> GetById(EvalMasterBaseReq req)
        {
            var obj = await GetAsync(c => c.EvalMasterId == req.EvalMasterId);
            if(obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

        public async Task<List<EvalDetail>> GetEvalDetailByMasterId(int MassterId)
        {
            var obj = await EvalDetailBE.GetAsync(c => c.EvalMasterId == MassterId);
            if (obj != null
               && obj.Any())
            {
                return obj.ToList();
            }
            return null;
        }

    }
}
