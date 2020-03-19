using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Commons;
using EVE.Data;

namespace EVE.Bussiness
{
    public class UserGroupBE : BaseBE<UserGroup>, IUserGroupBE
    {
        private readonly IFormsBE FormsBE;
        private readonly IUserGroupFormBE UserGroupFormBE;
        public UserGroupBE(IUnitOfWork<EVEEntities> uoW,
            IFormsBE formsBE,
            IUserGroupFormBE userGroupFormBE) : base(uoW)
        {
            FormsBE = formsBE;
            UserGroupFormBE = userGroupFormBE;
        }
        public async Task<UserGroup> GetById(UserGroupBaseReq req)
        {
            var obj = await GetAsync(c => c.UserGroupCode == req.UserGroupCode);
            if (obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

        public async Task<List<UserGroup>> GetByUserGroup(UserGroupBaseReq userGroup)
        {
            var obj = await GetAllAsync();
            if (obj != null && obj.Any())
            {
                if(userGroup.UserGroupCode == EnumUserGroup.SystemIT )
                    
                {
                    return obj.ToList();
                }
                else if( userGroup.UserGroupCode == EnumUserGroup.EduMinistry)
                {
                    return obj.Where(p => p.EduLevelCode != EnumEduLevelCode.TAdmin).ToList();
                }
                else if(userGroup.UserGroupCode == EnumUserGroup.EduProvince)
                {
                    return obj.Where(p=>p.EduLevelCode != EnumEduLevelCode.Ministry || p.EduLevelCode != EnumEduLevelCode.TAdmin).ToList();
                }
                else if(userGroup.UserGroupCode == EnumUserGroup.EduDepartment)
                {
                    return obj.Where(p => p.EduLevelCode != EnumEduLevelCode.Ministry 
                    || p.EduLevelCode != EnumEduLevelCode.TAdmin
                    || p.EduLevelCode != EnumEduLevelCode.Province
                    ).ToList();
                }
                else 
                {
                    return obj.Where(p => p.EduLevelCode != EnumEduLevelCode.Ministry
                    || p.EduLevelCode != EnumEduLevelCode.TAdmin
                    || p.EduLevelCode != EnumEduLevelCode.Province
                    || p.EduLevelCode != EnumEduLevelCode.Department
                    ).ToList();
                }

            }

            return null;
        }

        public async Task<List<Form>> GetFormsByUserGroup(UserGroupBaseReq userGroup)
        {
            var obj = await UserGroupFormBE.GetAsync(c => c.UserGroupCode == userGroup.UserGroupCode);
            if (obj != null && obj.Any())
            {
                var lstForm = obj.Select(q => q.FormCode).ToList();
                var objForms = await FormsBE.GetAsync(p => lstForm.Contains(p.FormCode));
                if (objForms != null && objForms.Any())
                {
                    return objForms.ToList();
                }
            }

            return null;
        }

    }
}
