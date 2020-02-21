using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public class EduDepartmentBE : BaseBE<EduDepartment>, IEduDepartmentBE
    {
        public EduDepartmentBE(IUnitOfWork<EVEEntities> uoW) : base(uoW)
        {
        }
        public async Task<EduDepartment> GetById(EduDepartmentBaseReq req)
        {
            var obj = await GetAsync(c => c.EduDepartmentId == req.EduDepartmentId);
            if(obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

        public async Task<List<EduDepartment>> GetByEduProvinceId(EduProvinceBaseReq req)
        {
            var obj = await GetAsync(c => c.EduProvinceId == req.EduProvinceId);
            if (obj != null
               && obj.Any())
            {
                return obj.ToList();
            }

            return null;
        }

    }
}
