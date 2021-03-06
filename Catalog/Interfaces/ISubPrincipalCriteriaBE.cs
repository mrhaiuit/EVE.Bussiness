﻿using System.Collections.Generic;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.ApiModels.Catalog.Response;
using EVE.Data;

namespace EVE.Bussiness
{
    public interface ISubPrincipalCriteriaBE : IBaseBE<SubPrincipalCriteria>
    {
        Task<SubPrincipalCriteria> GetById(SubPrincipalCriteriaBaseReq req);
        Task<List<SubPrincipalEmployeeAndPeriodRes>> GetByEmployeeAndPeriod(GetByEmployeeAndPeriodReq req);
    }
}
