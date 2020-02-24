using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;
using ExtensionMethods;

namespace EVE.Bussiness
{
    public class EvalMasterBE : BaseBE<EvalMaster>, IEvalMasterBE
    {
        private IEvalDetailBE EvalDetailBE { get; set; }
        private IEvalCriteriaBE EvalCriteriaBE { get; set; }
        private IEvalPeriodBE EvalPeriodBE { get; set; }
        private IEvalStandardBE EvalStandardBE { get; set; }
        public EvalMasterBE(IUnitOfWork<EVEEntities> uoW,
                            IEvalDetailBE evalDetailBE,
                            IEvalCriteriaBE evalCriteriaBE,
                            IEvalPeriodBE evalPeriodBE,
                            IEvalStandardBE evalStandardBE) : base(uoW)
        {
            EvalDetailBE = evalDetailBE;
            EvalCriteriaBE = evalCriteriaBE;
            EvalPeriodBE = evalPeriodBE;
            EvalStandardBE = evalStandardBE;
        }
        public async Task<EvalMaster> GetById(EvalMasterBaseReq req)
        {
            var obj = await GetAsync(c => c.EvalMasterId == req.EvalMasterId);
            if(obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

        public async Task<EvalMaster> GetPeriodAndEmployee(EvalMasterBaseReq req)
        {
            var obj = await GetAsync(c => c.EvalMasterId == req.EvalMasterId);
            if (obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

        public async Task<EvalMaster> GetByPeriodAndEmployee(EvalMasterGetByPeriodAndEmployeeReq req)
        {
            var obj = await GetAsync(c => c.BeEvalEmployeeId == req.BeEvalEmployeeId
                                            && c.EvalEmployeeId == req.EvalEmployeeId
                                            && c.EvalPeriodId == req.PeriodId);
            if (obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

        public async Task<List<EvalDetail>> GetEvalDetailByMasterId(EvalMasterBaseReq req)
        {
            if (req == null)
                return null;
            var obj = await EvalDetailBE.GetAsync(c => c.EvalMasterId == req.EvalMasterId);
            if (obj != null && obj.Any())
            {
                return obj.ToList();
            }

            return null;
        }

        public async Task<List<EvalDetail>> ExeEvalDetailByMasterId(ExeEvalDetailByMasterIdReq req)
        {
            if (req == null)
                return null;

            var evalMaster = (await GetAsync(p => p.BeEvalEmployeeId == req.BeEvalEmployeeId
                                                    && p.EvalEmployeeId == req.EvalEmployeeId
                                                    && p.EvalPeriodId == req.PeriodId))?.FirstOrDefault();
            if (evalMaster == null)
            {
                var obj = new EvalMaster()
                {
                    EvalPeriodId = req.PeriodId,
                    BeEvalEmployeeId = req.BeEvalEmployeeId,
                    EvalEmployeeId = req.EvalEmployeeId,
                    CreateBy = req.EvalEmployeeId,
                    CreateDate = DateTime.Now
                };
                this.Insert(obj);
                this._uoW.Save();
                evalMaster = (await GetAsync(p => p.EvalEmployeeId == req.EvalEmployeeId
                                                && p.BeEvalEmployeeId == req.BeEvalEmployeeId
                                                && p.EvalPeriodId == req.PeriodId))?.FirstOrDefault();
            }
            var lstCaterial = await GetEvalDetailByMasterId(new EvalMasterBaseReq() { EvalMasterId = evalMaster.EvalMasterId });
            if (lstCaterial != null && lstCaterial.Any())
            {
                return lstCaterial;
            }
            else
            {
                var period = await EvalPeriodBE.GetById(new EvalPeriodBaseReq() { EvalPeriodId = evalMaster.EvalPeriodId ?? 0 });
                if (period == null)
                    return null;
                var evalStandard = (await EvalStandardBE.GetAsync(p => p.EvalTypeCode == period.EvalTypeCode))
                    ?.Select(p => p.EvalStandardId).ToList();
                if (evalStandard == null)
                    return null;
                var caterials = await EvalCriteriaBE.GetAsync(p => p.Active != false
                                                                    && evalStandard.Contains(p.EvalStandardId??0)); 
                if (caterials == null)
                    return null;
                foreach (var item in caterials)
                {
                    var objDt = new EvalDetail()
                    {
                        EvalMasterId = evalMaster.EvalMasterId,
                        EvalStandardId = item.EvalStandardId,
                        EvalStandardName = (await EvalStandardBE.GetById(new EvalStandardBaseReq() { EvalStandardId = item.EvalStandardId.CheckInt() }))?.EvalStandardName,
                        EvalCriteriaId = item.EvalCriteriaId,
                        EvalCriteriaName = item.EvalCriteriaName,

                    };
                    EvalDetailBE.Insert(objDt);
                }
                //Luu cac detail
                if (!_uoW.Save())
                    return null;

                return await GetEvalDetailByMasterId(new EvalMasterBaseReq() { EvalMasterId = evalMaster.EvalMasterId });
            }
        }

    }
}
