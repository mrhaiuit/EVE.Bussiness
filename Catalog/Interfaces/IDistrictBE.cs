using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IDistrictBE : IBaseBE<District>
    {
        Task<District> GetById(DistrictBaseReq req);
    }
}
