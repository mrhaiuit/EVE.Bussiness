using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public class ReportBE :  IReportBE
    {
        private IUnitOfWork<EVEEntities> unitOfWork { get; set; }

        public ReportBE(IUnitOfWork<EVEEntities> uoW)
        {
            unitOfWork = uoW;
        }

        public async Task<List<usp_rpt_BM02_Result>> rptBm02(BM2Req req)
        {
            var result = await Task.Run(() => unitOfWork.Context.usp_rpt_BM02(req.PeriodId, req.EmployeeId));
            return result;
        }

        public async Task<List<usp_rpt_BM04_Result>> rptBm04(BM4Req req)
        {
            var result = await Task.Run(() => unitOfWork.Context.usp_rpt_BM04(req.SchoolId, req.PeriodId));
            return result;
        }

        public async Task<List<usp_rpt_BM05_Result>> rptBm05(EvalPeriodBaseReq req)
        {
            var result = await Task.Run(() => unitOfWork.Context.usp_rpt_BM05(req.EvalPeriodId));
            return result;
        }

    }
}
