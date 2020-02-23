using System.Collections.Generic;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IEduDepartmentBE : IBaseBE<EduDepartment>
    {
        Task<List<EduDepartment>> GetByUserGroupEmployee(UserGroupEmployeeReq req);
        Task<EduDepartment> GetById(EduDepartmentBaseReq req);
        Task<List<EduDepartment>> GetByEduProvinceId(EduProvinceBaseReq req);
    }
}
