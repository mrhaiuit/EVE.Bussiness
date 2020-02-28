using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public class FormBE : BaseBE<Form>, IFormsBE
    {
        public IUserGroupFormBE UserGroupFormBE { get; set; }
        public FormBE(IUnitOfWork<EVEEntities> uoW,
                    IUserGroupFormBE userGroupFormBE) : base(uoW)
        {
            UserGroupFormBE = userGroupFormBE;
        }
        public async Task<Form> GetById(GetByUserGroupBaseReq req)
        {
            var obj = await GetAsync(c => c.FormCode == req.FormCode);
            if(obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

        public async Task<List<Form>> GetFormsByPermission(UserGroupBaseReq req)
        {
            var lstForm = (await UserGroupFormBE.GetAsync(p => p.UserGroupCode == req.UserGroupCode))?.Select(p=>p.FormCode);
            if(lstForm ==null|| !lstForm.Any())
            {
                return null;
            }
            var obj = await GetAsync(c => lstForm.Contains(c.FormCode));
            if (obj != null
               && obj.Any())
            {
                return obj.ToList();
            }

            return null;
        }

    }
}
