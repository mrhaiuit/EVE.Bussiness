using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Authentication.Request;
using EVE.ApiModels.Catalog;
using EVE.ApiModels.Catalog.Response;
using EVE.Commons;
using EVE.Data;

namespace EVE.Bussiness
{
    public class EvalPeriodBE : BaseBE<EvalPeriod>, IEvalPeriodBE
    {

        private IUserGroupBE UserGroupBE { get; set; }
        private IEmployeeBE EmployeeBE { get; set; }
        public EvalPeriodBE(IUnitOfWork<EVEEntities> uoW,
                            IUserGroupBE userGroupBE,
                            IEmployeeBE employeeBE) : base(uoW)
        {
            UserGroupBE = userGroupBE;
            EmployeeBE = employeeBE;
        }

        // EvalPeriodByUserGroupAndTypeReq

        public async Task<List<EvalPeriod>> GetByUserGroupAndType(EvalPeriodByUserGroupAndTypeReq req)
        {
            var userGroup = await UserGroupBE.GetById(new UserGroupBaseReq() { UserGroupCode = req.UserGroupCode });
            if (userGroup == null)
                return null;
            var employee = await EmployeeBE.GetById(new EmployeeBaseReq { EmployeeId = req.EmpoyeeId });
            if (employee == null)
                return null;
            var result = new List<EvalPeriod>();
            if (userGroup.EduLevelCode == EnumEduLevelCode.School)
            {
                result = (await GetAsync(p => p.SchoolId == employee.SchoolId && p.EvalTypeCode == req.EvalTypeCode))?.ToList();

                //if (userGroup.UserGroupCode == EnumUserGroup.SchoolTeacher)
                //    result = (await GetAsync(p => p.SchoolId == employee.SchoolId && p.EvalTypeCode == EnumEvalType.Teacher))?.ToList();
                //else if (userGroup.UserGroupCode == EnumUserGroup.SubSchoolPrimary
                //    || userGroup.UserGroupCode == EnumUserGroup.SchoolPrimary)
                //    result = (await GetAsync(p => p.SchoolId == employee.SchoolId && p.EvalTypeCode == EnumEvalType.Primary))?.ToList();
                //else if (userGroup.UserGroupCode == EnumUserGroup.Officer)
                //    result = (await GetAsync(p => p.SchoolId == employee.SchoolId && p.EvalTypeCode == EnumEvalType.Primary))?.ToList();
                //else
                //    result = (await GetAsync(p => p.SchoolId == employee.SchoolId))?.ToList();

            }
            else
                result = null;
            if (result != null)
            {
                foreach (var p in result)
                {
                    p.EvalMasters = null;
                }
            }
            return result;
        }



        public async Task<List<EvalPeriod>> GetByUserGroupEmployee(UserGroupEmployeeReq req)
        {
            var userGroup = await UserGroupBE.GetById(new UserGroupBaseReq() { UserGroupCode = req.UserGroupCode });
            if (userGroup == null)
                return null;
            var employee = await EmployeeBE.GetById(new EmployeeBaseReq { EmployeeId = req.EmpoyeeId });
            if (employee == null)
                return null;
            var result = new List<EvalPeriod>();
            if (userGroup.EduLevelCode == EnumEduLevelCode.School)
            {
                if (userGroup.UserGroupCode == EnumUserGroup.SchoolTeacher)
                    result = (await GetAsync(p => p.SchoolId == employee.SchoolId && p.EvalTypeCode == EnumEvalType.Teacher))?.ToList();
                else if (userGroup.UserGroupCode == EnumUserGroup.SubSchoolPrimary
                    || userGroup.UserGroupCode == EnumUserGroup.SchoolPrimary)
                    result = (await GetAsync(p => p.SchoolId == employee.SchoolId && p.EvalTypeCode == EnumEvalType.Primary))?.ToList();
                else if(userGroup.UserGroupCode == EnumUserGroup.Officer)
                    result = (await GetAsync(p => p.SchoolId == employee.SchoolId && p.EvalTypeCode == EnumEvalType.Primary))?.ToList();
                else
                    result = (await GetAsync(p => p.SchoolId == employee.SchoolId))?.ToList();
            }
            else
                result = null;
            if(result!=null)
            {
                foreach(var p in result)
                {
                    p.EvalMasters = null;
                }    
            }    
            return result;
        }


        public async Task<EvalPeriod> GetById(EvalPeriodBaseReq req)
        {
            var obj = await GetAsync(c => c.EvalPeriodId == req.EvalPeriodId);
            if (obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

        public List<usp_GetPeriodByYearAndSchool_Result> GetByYearAndSchool(EvalPeriodGetByYearAndSchoolReq req)
        {
            var obj = _uoW.Context.usp_GetPeriodByYearAndSchool(req.Year, req.SchoolId, req.EvalTypeCode);
            if (obj != null
               && obj.Any())
            {
                return obj;
            }
            return null;
        }
        
    }
}
