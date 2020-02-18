using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IUserGroupFormBE : IBaseBE<UserGroup_Form>
    {
        Task<UserGroup_Form> GetById(UserGroupFormBaseReq req);
    }
}
