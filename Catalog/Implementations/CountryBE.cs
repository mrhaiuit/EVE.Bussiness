using System;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public class CountryBE : BaseBE<Country>, ICountryBE
    {
        public CountryBE(IUnitOfWork<EVEEntities> uoW) : base(uoW)
        {
        }
        public async Task<Country> GetById(CountryBaseReq req)
        {
            var obj = await GetAsync(c => c.CountryCode == req.CountryCode);
            if(obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

    }
}
