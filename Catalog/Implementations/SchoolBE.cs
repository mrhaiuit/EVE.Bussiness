using System;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public class SchoolBE : BaseBE<School>, ISchoolBE
    {
        public SchoolBE(IUnitOfWork<EVEEntities> uoW) : base(uoW)
        {
        }
        public async Task<School> GetById(SchoolBaseReq req)
        {
            var obj = await GetAsync(c => c.SchoolId == req.SchoolId);
            if(obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

    }
}
