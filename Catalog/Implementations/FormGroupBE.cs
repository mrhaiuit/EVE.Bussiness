using System;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public class FormGroupBE : BaseBE<FormGroup>, IFormGroupBE
    {
        public FormGroupBE(IUnitOfWork<EVEEntities> uoW) : base(uoW)
        {
        }
        public async Task<FormGroup> GetById(FormGroupBaseReq req)
        {
            var obj = await GetAsync(c => c.GroupCode == req.GroupCode);
            if(obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

    }
}
