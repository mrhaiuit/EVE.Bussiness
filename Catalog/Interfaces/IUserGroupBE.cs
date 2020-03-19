using System.Collections.Generic;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IUserGroupBE : IBaseBE<UserGroup>
    {
        Task<List<Form>> GetFormsByUserGroup(UserGroupBaseReq userGroup);
        Task<UserGroup> GetById(UserGroupBaseReq req);
        Task<List<UserGroup>> GetByUserGroup(UserGroupBaseReq userGroup);
    }
}
