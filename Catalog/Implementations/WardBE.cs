using System;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public class WardBE : BaseBE<Ward>, IWardBE
    {
        public WardBE(IUnitOfWork<EVEEntities> uoW) : base(uoW)
        {
        }
        public async Task<Ward> GetById(WardBaseReq req)
        {
            var obj = await GetAsync(c => c.WardId == req.WardId);
            if(obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

    }
}
