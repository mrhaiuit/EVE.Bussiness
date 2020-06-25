using System.Collections.Generic;
using System.Threading.Tasks;
using EVE.ApiModels.Authentication.Request;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IEmployeeBE : IBaseBE<Employee>
    {
        Task<List<Employee>> GetSubPrincipals(SchoolBaseReq req);
        Task<Employee> GetByUserName(UserNameReq req);
        Task<Employee> GetByUserName(string userName);
        Task<Employee> GetById(EmployeeBaseReq req);
        Task<List<Employee>> GetByUserGroupEmployee(UserGroupEmployeeReq req);
    }
}
