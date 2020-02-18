using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface ISchoolBE : IBaseBE<School>
    {

        Task<School> GetById(SchoolBaseReq req);
    }
}
