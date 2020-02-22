using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public class PositionBE : BaseBE<Position>, IPositionBE
    {
        public PositionBE(IUnitOfWork<EVEEntities> uoW) : base(uoW)
        {
        }
        public async Task<Position> GetById(PositionBaseReq req)
        {
            var obj = await GetAsync(c => c.PositionId == req.PositionId);
            if(obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

        public async Task<List<Position>> GetByEduLevel(PositionByEduLevelReq req)
        {
            var obj = await GetAsync(c => c.EduLevelCode == req.EduLevelCode);
            if (obj != null
               && obj.Any())
            {
                return obj.ToList();
            }

            return null;
        }

    }
}
