using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.ApiModels.Catalog.Response;
using EVE.Data;
using EVE.Commons;

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
            var obj = (this._uoW.Context.EvalDetails.Where(c => c.EvalMaster.EvalPeriod.Year > req.Year - 2
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
            }
            ).ToList();

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

        public async Task<EvalResult> GetEverageResultByYearAndUser(EvalDetailByYearAndUserReq req)
        {

            var objGroups = (this._uoW.Context.EvalDetails.Where(c => c.EvalMaster.EvalPeriod.Year == req.Year
                                                                 && c.EvalCriteriaId == req.EvalCriteriaId
                                                                 && c.EvalMaster.EvalEmployeeId != req.EmployeeId
                                                                 && c.EvalMaster.BeEvalEmployeeId == req.EmployeeId))
                                                             .GroupBy(p => new
                                                             {
                                                                 p.EvalResultCode
                                                             })
                                                             .Select(p => new { EvalResultCode = p.Key.EvalResultCode, Value = p.Count() })
                                                             .ToList();

            if (objGroups == null)
                return null;

            var KhongDat = objGroups.Where(p => p.EvalResultCode == EnumEvalResult.KhongDat).Count();
            var Dat = objGroups.Where(p => p.EvalResultCode == EnumEvalResult.Dat).Count();
            var Kha = objGroups.Where(p => p.EvalResultCode == EnumEvalResult.Kha).Count();
            var Tot = objGroups.Where(p => p.EvalResultCode == EnumEvalResult.Tot).Count();
            var value = (Dat + Kha * 2 + Tot * 3) / (KhongDat + Kha + Dat + Tot);
            var result = (await EvalResultBE.GetAsync(p => p.Idx == value))?.FirstOrDefault();
            return result;
            
        }

        public async Task<string> GetGroupResultByYearAndUser(EvalDetailByYearAndUserReq req)
        {
            var objGroups = (this._uoW.Context.EvalDetails.Where(c => c.EvalMaster.EvalPeriod.Year == req.Year
                                                                && c.EvalCriteriaId == req.EvalCriteriaId
                                                                && c.EvalMaster.EvalEmployeeId != req.EmployeeId
                                                                && c.EvalMaster.BeEvalEmployeeId == req.EmployeeId))
                                                            .GroupBy(p => new
                                                            {
                                                                p.EvalResultCode
                                                            })
                                                            .Select(p => new { Key = p.Key.EvalResultCode, Value = p.Count() })
                                                            .ToList();

            if (objGroups == null)
                return "";

            string result = "";
            foreach (var item in _uoW.Context.EvalResults.OrderBy(p=>p.Idx))
            {
                var s = item.EvalResultCode;
                if (objGroups.Where(p => p.Key.TrimEx() == item.EvalResultCode).Any())
                    result += $"{item.EvalResultName}: {objGroups.FirstOrDefault(p => p.Key == item.EvalResultCode)?.Value}<br> ";
                else
                    result += $"{item.EvalResultName}: {0}<br> ";
            }

            var employee = this._uoW.Context.Employees.FirstOrDefault(p => p.EmployeeId == req.EmployeeId);
            var employeeInDepartments = this._uoW.Context.Employees.Where(p => p.SchoolDepartment.SchoolDepartmentId == employee.SchoolDepartmentId).Count();
            var EvalCompleted = this._uoW.Context.EvalMasters.Where(c => c.EvalPeriod.Year == req.Year
                                                                 && c.EvalEmployeeId == req.EmployeeId
                                                                 && c.Employee.SchoolDepartmentId == employee.SchoolDepartmentId).Count();
            if (employeeInDepartments == 0)
                result += "Không tồn tại nhân viên trong cùng phòng ban.";
            else
                result += $"Tỷ lệ: {EvalCompleted}/{employeeInDepartments}";
            return result;
        }

        public override bool Update(EvalDetail obj)
        {
            var objAvaiable = Get(p => p.EvalDetailId == obj.EvalDetailId);
            if (objAvaiable != null && objAvaiable.Any())
                return false;
            

            _repository.Insert(obj);
            return _uoW.Save();
        }

    }

    public class EvalResultCount
    {
        public string Key { get; set; }
        public int Value { get; set; }
    }
}
