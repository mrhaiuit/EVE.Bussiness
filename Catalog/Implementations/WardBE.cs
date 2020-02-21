using System;
using System.Collections.Generic;
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

        public async Task<List<Ward>> GetByDistrictId(DistrictBaseReq req)
        {
            var obj = await GetAsync(c => c.DistrictId == req.DistrictId);
            if (obj != null
               && obj.Any())
            {
                return obj.ToList();
            }

            return null;
        }

        public async Task<List<Ward>> GetByProvinceId(ProvinceBaseReq req)
        {
            var obj = await GetAsync(c => c.District.ProvinceId == req.ProvinceId);
            if (obj != null
               && obj.Any())
            {
                return obj.ToList();
            }

            return null;
        }
    }
}
