﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.ApiModels.Catalog.Response;
using EVE.Commons;
using EVE.Data;

namespace EVE.Bussiness
{
    public class EvalCriteriaBE : BaseBE<EvalCriteria>, IEvalCriteriaBE
    {
        private readonly IEvalGuideBE EvalGuideBE;
        public EvalCriteriaBE(IUnitOfWork<EVEEntities> uoW,
            IEvalGuideBE evalGuideBE
            ) : base(uoW)
        {
            EvalGuideBE = evalGuideBE;
        } 
        public async Task<EvalCriteria> GetById(EvalCriteriaBaseReq req)
        {
            var obj = await GetAsync(c => c.EvalCriteriaId == req.EvalCriteriaId);
            if(obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

        public async Task<List<EvalCriteria>> GetByStandardId(EvalStandardBaseReq req)
        {
            var obj = await GetAsync(c => c.EvalStandardId == req.EvalStandardId);
            if (obj != null
               && obj.Any())
            {
                return obj.ToList();
            }

            return null;
        }

        public async Task<List<EvalCriteriaSchoolLevelRes>> GetBySchoolLevel(GetByEvalTypeSchoolLevelReq req)
        {
            var obj = await Task.Run(() => from st in _uoW.Context.EvalStandards
                                           join c in _uoW.Context.EvalCriterias on st.EvalStandardId equals c.EvalStandardId
                                           where st.SchoolLevelCode == req.SchoolLevelCode && st.EvalTypeCode == EnumEvalType.Primary
                                           select new EvalCriteriaSchoolLevelRes
                                           {
                                               Active = c.Active,
                                               EvalCriteriaCode = c.EvalCriteriaCode,
                                               EvalCriteriaId = c.EvalCriteriaId,
                                               EvalCriteriaName = c.EvalCriteriaName,
                                               EvalStandardId=c.EvalStandardId,
                                               EvalStandardName=st.EvalStandardName
                                               
                                           });
            if (obj != null
               && obj.Any())
            {
                return obj.ToList();
            }

            return null;
        }

        public async Task<EvalGuide> GetGuideOfCriteria(GetGuideOfCriteriaReq req)
        {
            var obj = await EvalGuideBE.GetAsync(c => c.EvalCriteriaId == req.EvalCriteriaId && c.EvalResultCode == req.EvalResultCode);
            if (obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

    }
}
