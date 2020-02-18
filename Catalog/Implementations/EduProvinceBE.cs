using System;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public class EduProvinceBE : BaseBE<EduProvince>, IEduProvinceBE
    {
        public EduProvinceBE(IUnitOfWork<EVEEntities> uoW) : base(uoW)
        {
        }
        public async Task<EduProvince> GetById(EduProvinceBaseReq req)
        {
            var obj = await GetAsync(c => c.EduProvinceId == req.EduProvinceId);
            if(obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

    }
}
