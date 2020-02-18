using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface ILoginUserBE : IBaseBE<LoginUser>
    {

        Task<LoginUser> GetById(LoginUserBaseReq req);
    }
}
