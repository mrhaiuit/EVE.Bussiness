using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface ICountryBE : IBaseBE<Country>
    {
        Task<Country> GetById(CountryBaseReq req);
    }
}
