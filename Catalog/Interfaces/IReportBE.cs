using System.Collections.Generic;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface IReportBE
    {
        Task<List<usp_rpt_BM02_Result>> rptBm02(BM2Req req);

        Task<List<usp_rpt_BM04_Result>> rptBm04(BM4Req req);
    }
}
