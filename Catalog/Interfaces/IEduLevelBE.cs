using System.Collections.Generic;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IEduLevelBE : IBaseBE<EduLevel>
    {
        Task<List<EduLevel>> GetByUserGroup(UserGroupBaseReq req);
        Task<EduLevel> GetById(EduLevelBaseReq req);
    }
}
