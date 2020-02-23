using System.Collections.Generic;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface ISchoolDepartmentBE : IBaseBE<SchoolDepartment>
    {
        Task<List<SchoolDepartment>> GetBySchoolId(SchoolBaseReq req);
        Task<SchoolDepartment> GetById(SchoolDepartmentBaseReq req);
    }
}
