using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IUserGroupEmployeeBE : IBaseBE<UserGroup_Employee>
    {

        Task<UserGroup_Employee> GetById(UserGroupEmployeeBaseReq req);
    }
}
