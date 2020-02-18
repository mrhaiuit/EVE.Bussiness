using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IWardBE : IBaseBE<Ward>
    {
        Task<Ward> GetById(WardBaseReq req);
    }
}
