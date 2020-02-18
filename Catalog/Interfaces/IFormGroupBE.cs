using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IFormGroupBE : IBaseBE<FormGroup>
    {

        Task<FormGroup> GetById(FormGroupBaseReq req);
    }
}
