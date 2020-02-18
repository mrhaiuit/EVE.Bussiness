using System.Collections.Generic;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IUserGroupBE : IBaseBE<UserGroup>
    {
        Task<List<Form>> GetFormsByUserGroup(string userGroup);
        Task<UserGroup> GetById(UserGroupBaseReq req);
    }
}
