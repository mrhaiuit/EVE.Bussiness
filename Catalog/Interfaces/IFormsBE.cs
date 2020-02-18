using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IFormsBE : IBaseBE<Form>
    {

        Task<Form> GetById(FormsBaseReq req);
    }
}
