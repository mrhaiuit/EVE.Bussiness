using System;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public class DistrictBE : BaseBE<District>, IDistrictBE
    {
        public DistrictBE(IUnitOfWork<EVEEntities> uoW) : base(uoW)
        {
        }
        public async Task<District> GetById(DistrictBaseReq req)
        {
            var obj = await GetAsync(c => c.DistrictId == req.DistrictId);
            if(obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

    }
}
