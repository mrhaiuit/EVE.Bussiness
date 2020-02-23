using System;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Authentication.Request;
using EVE.Commons;
using EVE.Data;

namespace EVE.Bussiness
{
    public class EmployeeBE : BaseBE<Employee>, IEmployeeBE
    {
        public EmployeeBE(IUnitOfWork<EVEEntities> uoW) : base(uoW)
        {
        }

        new public bool Insert(Employee obj)
        {
            var objAvaiable = Get(p => p.UserName == obj.UserName);
            if (objAvaiable != null && objAvaiable.Any())
                return false;
            obj.Password = obj.Password.EncodePassword();
            _repository.Insert(obj);
            return _uoW.Save();
        }

        public async Task<Employee> GetByUserName( UserNameReq req)
        {
            var obj = await GetAsync(c => c.UserName == req.UserName);
            if (obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

        public async Task<Employee> GetById(EmployeeBaseReq req)
        {
            var obj = await GetAsync(c => c.EmployeeId == req.EmployeeId);
            if(obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

        public async Task<Employee> GetByUserName(string userName)
        {
            var obj = await GetAsync(c => c.UserName == userName);
            if (obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

    }
}
