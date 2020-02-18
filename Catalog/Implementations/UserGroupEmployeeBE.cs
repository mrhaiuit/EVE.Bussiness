using System;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public class UserGroupEmployeeBE : BaseBE<UserGroup_Employee>, IUserGroupEmployeeBE
    {
        public UserGroupEmployeeBE(IUnitOfWork<EVEEntities> uoW) : base(uoW)
        {
        }
        public async Task<UserGroup_Employee> GetById(UserGroupEmployeeBaseReq req)
        {
            var obj = await GetAsync(c => c.UserGroupCode == req.UserGroupCode && c.EmployeeId == req.EmployeeId);
            if(obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

    }
}
