using System.Collections.Generic;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IWardBE : IBaseBE<Ward>
    {
        Task<List<Ward>> GetByProvinceId(ProvinceBaseReq req);
        Task<Ward> GetById(WardBaseReq req);
        Task<List<Ward>> GetByDistrictId(DistrictBaseReq req);
    }
}
