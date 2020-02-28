using System.Collections.Generic;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IFormsBE : IBaseBE<Form>
    {
        Task<List<Form>> GetFormsByPermission(UserGroupBaseReq req);
        Task<Form> GetById(GetByUserGroupBaseReq req);
    }
}
