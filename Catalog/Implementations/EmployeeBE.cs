using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Authentication.Request;
using EVE.ApiModels.Catalog;
using EVE.Commons;
using EVE.Data;

namespace EVE.Bussiness
{
    public class EmployeeBE : BaseBE<Employee>, IEmployeeBE
    {
        private IUserGroupBE UserGroupBE { get; set; }
        public EmployeeBE(IUnitOfWork<EVEEntities> uoW,
                            IUserGroupBE userGroupBE
                            ) : base(uoW)
        {
            UserGroupBE = userGroupBE;
        }

        public async Task<List<Employee>> GetSubPrincipals(SchoolBaseReq req)
        {
            return (await GetAsync(p => p.SchoolId == req.SchoolId && p.UserGroupCode == EnumUserGroup.SubSchoolPrimary))?.ToList();
        }


        public async Task<List<Employee>> GetAllPrincipals(SchoolBaseReq req)
        {
            return (await GetAsync(p => p.SchoolId == req.SchoolId
            && (p.UserGroupCode == EnumUserGroup.SubSchoolPrimary || p.UserGroupCode == EnumUserGroup.SchoolPrimary)
            ))?.ToList();
        }

        public async Task<List<Employee>> GetByUserGroupEmployee(UserGroupEmployeeReq req)
        {
            var userGroup = await UserGroupBE.GetById(new UserGroupBaseReq() { UserGroupCode = req.UserGroupCode });
            if (userGroup == null)
                return null;
            var employee = await GetById(new EmployeeBaseReq { EmployeeId = req.EmpoyeeId });
            if (employee == null)
                return null;
            var result = new List<Employee>();
            if (userGroup.EduLevelCode == EnumEduLevelCode.School)
            {
                if (userGroup.UserGroupCode == EnumUserGroup.SchoolTeacher
                    || userGroup.UserGroupCode == EnumUserGroup.LeadSubject)
                    result = (await GetAsync(p => p.SchoolId == employee.SchoolId
                                                && p.SchoolDepartmentId == employee.SchoolDepartmentId
                                                && p.UserGroupCode == EnumUserGroup.SchoolTeacher))?.ToList();
                else if (userGroup.UserGroupCode == EnumUserGroup.SchoolPrimary)
                    result = (await GetAsync(p => p.SchoolId == employee.SchoolId
                                                && p.UserGroupCode == EnumUserGroup.SchoolTeacher))?.ToList();
                else

                    result = (await GetAsync(p => p.SchoolId == employee.SchoolId))?.ToList();
            }
            else
                result = null;
            return result;
        }

        new public bool Insert(Employee obj)
        {
            var objAvaiable = Get(p => p.UserName == obj.UserName);
            if (objAvaiable != null && objAvaiable.Any())
                return false;
            obj.Password = obj.Password.EncodePassword();
            _repository.Insert(obj);
            return _uoW.Save();
        }

        public async Task<Employee> GetByUserName( UserNameReq req)
        {
            var obj = await GetAsync(c => c.UserName == req.UserName);
            if (obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

        public async Task<Employee> GetById(EmployeeBaseReq req)
        {
            var obj = await GetAsync(c => c.EmployeeId == req.EmployeeId);
            if(obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

        public async Task<Employee> GetByUserName(string userName)
        {
            var obj = await GetAsync(c => c.UserName == userName);
            if (obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

    }
}
