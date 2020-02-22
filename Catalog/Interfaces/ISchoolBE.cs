using System.Collections.Generic;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface ISchoolBE : IBaseBE<School>
    {
        Task<List<School>> GetByUserGroupEmployee(UserGroupEmployeeReq req);
        Task<List<School>> GetByEduProvinceId(EduProvinceBaseReq req);
        Task<School> GetById(SchoolBaseReq req);
        Task<List<School>> GetByEduDepartmentId(EduDepartmentBaseReq req);
    }
}
