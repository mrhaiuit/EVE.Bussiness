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
    public class EduDepartmentBE : BaseBE<EduDepartment>, IEduDepartmentBE
    {

        private IUserGroupBE UserGroupBE { get; set; }
        private IEmployeeBE EmployeeBE { get; set; }
        public EduDepartmentBE(IUnitOfWork<EVEEntities> uoW,
            IUserGroupBE userGroupBE,
            IEmployeeBE employeeBE) : base(uoW)
        {
            UserGroupBE = userGroupBE;
            EmployeeBE = employeeBE;
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


        public async Task<List<EduDepartment>> GetByUserGroupEmployee(UserGroupEmployeeReq req)
        {
            var userGroup = await UserGroupBE.GetById(new UserGroupBaseReq() { UserGroupCode = req.UserGroupCode });
            if (userGroup == null)
                return null;
            var employee = await EmployeeBE.GetById(new EmployeeBaseReq { EmployeeId = req.EmpoyeeId });
            if (employee == null)
                return null;
            var result = new List<EduDepartment>();
            if (userGroup.EduLevelCode == EnumEduLevelCode.Ministry
                || userGroup.EduLevelCode == EnumEduLevelCode.TAdmin)
            {
                result = (await GetAllAsync())?.ToList();
            }
            else if (userGroup.EduLevelCode == EnumEduLevelCode.Province)
            {
                result = (await GetAsync(p => p.EduProvinceId == employee.EduProvinceId))?.ToList();
            }
            else if (userGroup.EduLevelCode == EnumEduLevelCode.Department
                || userGroup.EduLevelCode == EnumEduLevelCode.School)
            {
                result = (await GetAsync(p => p.EduDepartmentId == employee.EduDepartmentId))?.ToList();
            }
            else
                result = null;
            return result;
        }

    }
}
