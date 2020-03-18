using System;
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



    }
}
