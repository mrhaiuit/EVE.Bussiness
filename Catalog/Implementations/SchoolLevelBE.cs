﻿using System;
using System.Linq;
using System.Threading.Tasks;
using EVE.ApiModels.Catalog;
using EVE.Data;

namespace EVE.Bussiness
{
    public class SchoolLevelBE : BaseBE<SchoolLevel>, ISchoolLevelBE
    {
        public SchoolLevelBE(IUnitOfWork<EVEEntities> uoW) : base(uoW)
        {
        }
        public async Task<SchoolLevel> GetById(SchoolLevelBaseReq req)
        {
            var obj = await GetAsync(c => c.SchoolLevelCode == req.SchoolLevelCode);
            if(obj != null
               && obj.Any())
            {
                return obj.FirstOrDefault();
            }

            return null;
        }

    }
}
