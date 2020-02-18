using System;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public class ProvinceBE : BaseBE<Province>, IProvinceBE
    {
        public ProvinceBE(IUnitOfWork<EVEEntities> uoW) : base(uoW)
        {
        }
        public async Task<Province> GetById(ProvinceBaseReq req)
        {
            var obj = await GetAsync(c => c.ProvinceId == req.ProvinceId);
            if(obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

    }
}
