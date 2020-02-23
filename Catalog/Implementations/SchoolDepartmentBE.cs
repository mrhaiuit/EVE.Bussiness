using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Authentication.Request;
using EVE.ApiModels.Catalog;
using EVE.Commons;
using EVE.Data;

namespace EVE.Bussiness
{
    public class SchoolDepartmentBE : BaseBE<SchoolDepartment>, ISchoolDepartmentBE
    {
        private IUserGroupBE UserGroupBE { get; set; }
        private IEmployeeBE EmployeeBE { get; set; }
        public SchoolDepartmentBE(IUnitOfWork<EVEEntities> uoW,
            IUserGroupBE userGroupBE,
            IEmployeeBE employeeBE) : base(uoW)
        {
            UserGroupBE = userGroupBE;
            EmployeeBE = employeeBE;
        }
        public async Task<SchoolDepartment> GetById(SchoolDepartmentBaseReq req)
        {
            var obj = await GetAsync(c => c.SchoolDepartmentId == req.SchoolDepartmentId);
            if (obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

        public async Task<List<SchoolDepartment>> GetBySchoolId(SchoolBaseReq req)
        {
            var obj = await GetAsync(c => c.SchoolId == req.SchoolId);
            if (obj != null
               && obj.Any())
            {
                return obj.ToList();
            }

            return null;
        }
    }
}
