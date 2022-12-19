using Lyubishchev.TimePeriodCategories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Lyubishchev.EntityFrameworkCore
{
    public class EfCoreTimePeriodCategoryRepository
        : EfCoreRepository<LyubishchevDbContext, TimePeriodCategory, Guid>,
        ITimePeriodCategoryRepository
    {
        public EfCoreTimePeriodCategoryRepository(
            IDbContextProvider<LyubishchevDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
        public async Task<TimePeriodCategory> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(tc => tc.Name == name);
        }

        public async Task<List<TimePeriodCategory>> GetListAsync(
            int skipCount, 
            int maxResultCount, 
            string sorting, 
            string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                tc => tc.Name.Contains(filter)
                )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
