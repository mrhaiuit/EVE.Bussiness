using System;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public class FormBE : BaseBE<Form>, IFormsBE
    {
        public FormBE(IUnitOfWork<EVEEntities> uoW) : base(uoW)
        {
        }
        public async Task<Form> GetById(GetByUserGroupBaseReq req)
        {
            var obj = await GetAsync(c => c.FormCode == req.FormCode);
            if(obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

    }
}
