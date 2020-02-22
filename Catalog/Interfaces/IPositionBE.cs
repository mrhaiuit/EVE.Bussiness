using System.Collections.Generic;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IPositionBE : IBaseBE<Position>
    {
        Task<List<Position>> GetByEduLevel(PositionByEduLevelReq req);
        Task<Position> GetById(PositionBaseReq req);
    }
}
