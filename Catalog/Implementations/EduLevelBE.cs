using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Commons;
using EVE.Data;

namespace EVE.Bussiness
{
    public class EduLevelBE : BaseBE<EduLevel>, IEduLevelBE
    {
        private IUserGroupBE UserGroupBE { get; set; }
        public EduLevelBE(IUnitOfWork<EVEEntities> uoW,
            IUserGroupBE userGroupBE) : base(uoW)
        {
            UserGroupBE = userGroupBE;
        } 
        public async Task<EduLevel> GetById(EduLevelBaseReq req)
        {
            var obj = await GetAsync(c => c.EduLevelCode == req.EduLevelCode);
            if(obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

        public async Task<List<EduLevel>> GetByUserGroup(UserGroupBaseReq req)
        {
            var userGroup = await UserGroupBE.GetById(req);
            if (userGroup == null)
                return null;
            var obj = await GetAllAsync();
            if (obj != null
               && obj.Any())
            {
                obj = obj.OrderBy(p => p.Idx);
                var result = new List<EduLevel>();
                if (userGroup.EduLevelCode == EnumEduLevelCode.Ministry)
                {
                    result = obj.ToList();
                }
                else if (userGroup.EduLevelCode == EnumEduLevelCode.Province)
                {
                    result = obj.Where(p => p.EduLevelCode != EnumEduLevelCode.Ministry).ToList();
                }
                else if (userGroup.EduLevelCode == EnumEduLevelCode.Department)
                {
                    result = obj.Where(p => p.EduLevelCode != EnumEduLevelCode.Ministry
                                            && p.EduLevelCode != EnumEduLevelCode.Province).ToList();
                }
                else if (userGroup.EduLevelCode == EnumEduLevelCode.School)
                {
                    result = obj.Where(p => p.EduLevelCode == EnumEduLevelCode.School).ToList();
                }
                else
                    result = obj.ToList();

                return result;
            }

            return null;
        }

    }
}
