using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.ApiModels.Catalog.Response;
using EVE.Data;

namespace EVE.Bussiness
{
    public class EvalDetailBE : BaseBE<EvalDetail>, IEvalDetailBE
    {
        private IEvalResultBE EvalResultBE { get; set; }
        public EvalDetailBE(IUnitOfWork<EVEEntities> uoW,
            IEvalResultBE evalResultBE) : base(uoW)
        {
            EvalResultBE = evalResultBE;
        }
        public async Task<EvalDetail> GetById(EvalDetailBaseReq req)
        {
            var obj = await GetAsync(c => c.EvalDetailId == req.EvalDetailId);
            if(obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }
        public async Task<List<EvalDetailByYearAndUserRes>> GetByByYearAndUser(EvalDetailByYearAndUserReq req)
        {
            var obj = (this._uoW.Context.EvalDetails.Where(c => c.EvalMaster.EvalPeriod.Year > req.Year - 1
                                                                && c.EvalCriteriaId == req.EvalCriteriaId
                                                                && c.EvalMaster.EvalEmployeeId == req.EmployeeId
                                                                && c.EvalMaster.BeEvalEmployeeId == req.EmployeeId))
            ?.Select(p => new
            {
                p.EvalCriteriaId,
                p.Sample,
                p.EvalResultCode,
                p.EvalMaster.EvalPeriod.Year,
                p.Attachment
            }).ToList();

            var lstResult = await EvalResultBE.GetAllAsync();
            if (obj != null
               && obj.Any())
            {
                return obj.Select(p => new EvalDetailByYearAndUserRes()
                {
                    Year = p.Year ?? 0,
                    EvalResultName = lstResult.FirstOrDefault(t => t.EvalResultCode == p.EvalResultCode).EvalResultName,
                    Reason = p.Sample,
                    Attachment = p.Attachment
                }).ToList();
            }

            return null;
        }

    }
}
