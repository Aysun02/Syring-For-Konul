using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class WhatWeDoBestRepository :Repository<WhatWeDoBest>, IWhatWeDoBestRepository
    {
        private readonly AppDbContext _appDbContext;

        public WhatWeDoBestRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}
