﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.ApiModels.Catalog.Response;
using EVE.Commons;
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
        private IEvalResultBE EvalResultBE { get; set; }
        private ISchoolBE SchoolBE { get; set; }
        public EvalMasterBE(IUnitOfWork<EVEEntities> uoW,
                            IEvalDetailBE evalDetailBE,
                            IEvalCriteriaBE evalCriteriaBE,
                            IEvalPeriodBE evalPeriodBE,
                            IEvalStandardBE evalStandardBE,
                            IEvalResultBE evalResultBE,
                            ISchoolBE schoolBE) : base(uoW)
        {
            EvalDetailBE = evalDetailBE;
            EvalCriteriaBE = evalCriteriaBE;
            EvalPeriodBE = evalPeriodBE;
            EvalStandardBE = evalStandardBE;
            EvalResultBE = evalResultBE;
            SchoolBE = schoolBE;
        }
        public async Task<EvalMaster> GetById(EvalMasterBaseReq req)
        {
            var obj = await GetAsync(c => c.EvalMasterId == req.EvalMasterId);
            if (obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

        public async Task<bool> CompleteFinal(EvalMasterBaseReq req)
        {
            var obj = await GetAsync(c => c.EvalMasterId == req.EvalMasterId);
            if (obj != null
               && obj.Any())
            {
                var update = obj.FirstOrDefault();
                update.IsFinal = true;
                return Update(update);
            }

            return false;
        }

        public async Task<bool> CancelFinal(EvalMasterBaseReq req)
        {
            var obj = await GetAsync(c => c.EvalMasterId == req.EvalMasterId);
            if (obj != null
               && obj.Any())
            {
                var update = obj.FirstOrDefault();
                update.IsFinal = false;
                return Update(update);
            }

            return false;
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

        public async Task<EvalResultSumaryRes> GetEvalResultSumary(ExeEvalDetailByMasterIdReq req)
        {
            if (req == null)
                return null;
            try
            {
                var evalMaster = (await GetAsync(p => p.BeEvalEmployeeId == req.BeEvalEmployeeId
                                                        && p.EvalEmployeeId == req.EvalEmployeeId
                                                        && p.EvalPeriodId == req.PeriodId))?.FirstOrDefault();
                if (evalMaster == null)
                {
                    return new EvalResultSumaryRes();
                }
                else
                {
                    var lstEvalResult = EvalResultBE.GetAll();
                    var lstCaterial = await GetEvalDetailByMasterId(new EvalMasterBaseReq() { EvalMasterId = evalMaster.EvalMasterId });
                    if (lstCaterial == null || !lstCaterial.Any())
                    {
                        return new EvalResultSumaryRes();
                    }
                    else
                    {
                        var totalCriteria = lstCaterial.Count();
                        var total = lstCaterial.Sum(p => lstEvalResult.FirstOrDefault(q => q.EvalResultCode == p.EvalResultCode)?.Idx).CheckInt();
                        var totalCriteriaHasValue = lstCaterial.Where(p => !string.IsNullOrEmpty(p.EvalResultCode)).Count();
                        var everage = Math.Round((total / totalCriteria).CheckDecimal(), MidpointRounding.AwayFromZero).CheckInt();
                        var evalResult = lstEvalResult.FirstOrDefault(p => p.Idx == everage);
                        return new EvalResultSumaryRes()
                        {
                            TotalCriteria = totalCriteria,
                            Total = total,
                            TotalCriteriaHasValue = totalCriteriaHasValue,
                            Everage = everage,
                            EvalResultCode = evalResult?.EvalResultCode,
                            EvalResultName = evalResult?.EvalResultName

                        };
                    }
                }
            }
            catch
            {
                return new EvalResultSumaryRes();
            }

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
                var periodId = period.SchoolId ?? 0;
                var school = await SchoolBE.GetById(new SchoolBaseReq() { SchoolId = periodId });
                var evalStandard = (await EvalStandardBE.GetAsync(p => p.EvalTypeCode == period.EvalTypeCode
                && p.SchoolLevelCode == school.SchoolLevelCode))?.Select(p => p.EvalStandardId).ToList();
                if (evalStandard == null)
                    return null;
                var caterials = await EvalCriteriaBE.GetAsync(p => p.Active != false
                                                                    && evalStandard.Contains(p.EvalStandardId ?? 0));
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

        public async Task<List<EvalMasterGetByUserIdRes>> GetSelfEvalByUserId(EvalMasterGetByUserIdReq req)
        {
            if (req == null)
                return null;
            var obj = (await GetAsync(c => c.BeEvalEmployeeId == req.EmployeeId
                                        && c.EvalEmployeeId == req.EmployeeId
                                        && (c.EvalPeriod.Year == req.Year || req.Year == 0)))
                                        .Select(p => new EvalMasterGetByUserIdRes()
                                        {
                                            MasterId = p.EvalMasterId,
                                            Year = p.EvalPeriod?.Year,
                                            FromDate = p.EvalPeriod.FromDate,
                                            ToDate = p.EvalPeriod.ToDate,
                                            Period = p.EvalPeriod.PeriodName,
                                            TotalEval = p.EvalDetails.Where(q => !q.EvalResultCode.IsNullOrEmpty()).Count(),
                                            EvalEmployee = p.Employee.EmployeeName,
                                            BeEvalEmployee = p.Employee1.EmployeeName,
                                            Approveed = p.IsFinal,
                                            Class = p.EvalResultCode
                                        });

            return obj.ToList();
        }

        public async Task<List<EvalMasterGetByUserIdRes>> GetEvalByUserId(EvalMasterGetByUserIdReq req)
        {
            if (req == null)
                return null;
            var obj = (await GetAsync(c => c.EvalEmployeeId == req.EmployeeId
                                        && (c.EvalPeriod.Year == req.Year || req.Year == 0)))
                                        .Select(p => new EvalMasterGetByUserIdRes()
                                        {
                                            MasterId = p.EvalMasterId,
                                            Year = p.EvalPeriod?.Year,
                                            FromDate = p.EvalPeriod.FromDate,
                                            ToDate = p.EvalPeriod.ToDate,
                                            Period = p.EvalPeriod.PeriodName,
                                            TotalEval = p.EvalDetails.Where(q => !q.EvalResultCode.IsNullOrEmpty()).Count(),
                                            EvalEmployee = p.Employee.EmployeeName,
                                            BeEvalEmployee = p.Employee1.EmployeeName,
                                            Approveed = p.IsFinal,
                                            Class = p.EvalResultCode
                                        }); ;

            return obj.ToList();
        }



    }
}
