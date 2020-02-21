using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public class SchoolBE : BaseBE<School>, ISchoolBE
    {
        public SchoolBE(IUnitOfWork<EVEEntities> uoW) : base(uoW)
        {
        }
        public async Task<School> GetById(SchoolBaseReq req)
        {
            var obj = await GetAsync(c => c.SchoolId == req.SchoolId);
            if(obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

        public async Task<List<School>> GetByEduDepartmentId(EduDepartmentBaseReq req)
        {
            var obj = await GetAsync(c => c.EduDepartmentId == req.EduDepartmentId);
            if (obj != null
               && obj.Any())
            {
                return obj.ToList();
            }

            return null;
        }

        public async Task<List<School>> GetByEduProvinceId(EduProvinceBaseReq req)
        {
            var obj = await GetAsync(c => c.EduProvinceId == req.EduProvinceId || c.EduDepartment.EduProvinceId == req.EduProvinceId);
            if (obj != null
               && obj.Any())
            {
                return obj.ToList();
            }

            return null;
        }

    }
}
