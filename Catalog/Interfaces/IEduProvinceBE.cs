using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IEduProvinceBE : IBaseBE<EduProvince>
    {

        Task<EduProvince> GetById(EduProvinceBaseReq req);
    }
}
