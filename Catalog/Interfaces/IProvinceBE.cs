using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IProvinceBE : IBaseBE<Province>
    {
        Task<Province> GetById(ProvinceBaseReq req);
    }
}
