using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface ISchoolLevelBE : IBaseBE<SchoolLevel>
    {

        Task<SchoolLevel> GetById(SchoolLevelBaseReq req);
    }
}
