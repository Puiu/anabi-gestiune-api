﻿using Anabi.DataAccess.Ef;
using Anabi.DataAccess.Ef.DbModels;
using Anabi.DataAccess.Abstractions.Repositories;

namespace Anabi.DataAccess.Repositories
{
    public class StagesRepository : GenericRepository<StageDb>
    {
        public StagesRepository(AnabiContext ctx) : base(ctx)
        {

        }

    }
}