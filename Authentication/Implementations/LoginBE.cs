using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Authentication.Request;
using EVE.Commons;
using EVE.Data;

namespace EVE.Bussiness
{
    public class LoginBE : BaseBE<Employee>, ILoginBE
    {
        private IUserGroupEmployeeBE UserGroupEmployeeBE { get; set; }
        private IEmployeeBE EmployeeBE { get; set; }
        public LoginBE(IUnitOfWork<EVEEntities> uoW, 
            IUserGroupEmployeeBE userGroupEmployeeBE
            ) : base(uoW)
        {
            UserGroupEmployeeBE = userGroupEmployeeBE;
        }

        #region ILogonUserBE Members

        public async Task<Employee> GetEmployeeByAccount(LoginReq req)
        {
            try
            {
                req.PassWord = req.PassWord.EncodePassword();
                var users = await GetAsync(c => c.UserName == req.UserName && c.Password == req.PassWord);
                if (users != null
                   && users.Any())
                {
                    return users.FirstOrDefault();
                }
                return null;
            }
            catch(Exception ex)
            {

                return null;
            }
        }

        public async Task<bool> SaveLogin(LoginUser loginUser)
        {
            try
            {
                var logonRepository = _uoW.Repository<LoginUser>();
                var result = logonRepository.Insert(loginUser);
                if (result != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<List<UserGroup_Employee>> GetUserGroupByUserName(string userName)
        {
            var employee = (await GetAsync(p => p.UserName == userName))?.FirstOrDefault();
            if (employee == null)
                return null;
            var obj = await UserGroupEmployeeBE.GetAsync(p => p.EmployeeId == employee.EmployeeId);
            if (obj != null
               && obj.Any())
            {
                return obj.ToList();
            }

            return null;
        }

        #endregion
    }
}
