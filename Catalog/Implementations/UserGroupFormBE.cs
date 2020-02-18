using System;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public class UserGroupFormBE : BaseBE<UserGroup_Form>, IUserGroupFormBE
    {
        public UserGroupFormBE(IUnitOfWork<EVEEntities> uoW) : base(uoW)
        {
        }
        public async Task<UserGroup_Form> GetById(UserGroupFormBaseReq req)
        {
            var obj = await GetAsync(c => c.UserGroupCode == req.UserGroupCode && c.FormCode == req.FormCode);
            if(obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

    }
}
