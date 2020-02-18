using System;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public class LoginUserBE : BaseBE<LoginUser>, ILoginUserBE
    {
        public LoginUserBE(IUnitOfWork<EVEEntities> uoW) : base(uoW)
        {
        }
        public async Task<LoginUser> GetById(LoginUserBaseReq req)
        {
            var obj = await GetAsync(c => c.LoginId == req.LoginId);
            if(obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

    }
}
