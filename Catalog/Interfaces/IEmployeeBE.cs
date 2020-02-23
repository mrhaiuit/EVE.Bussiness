using System.Threading.Tasks;
using EVE.ApiModels.Authentication.Request;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IEmployeeBE : IBaseBE<Employee>
    {
        Task<Employee> GetByUserName(UserNameReq req);
        Task<Employee> GetByUserName(string userName);
        Task<Employee> GetById(EmployeeBaseReq req);
    }
}
