using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Lyubishchev.TimePeriodCategories
{
    public interface ITimePeriodCategoryRepository:IRepository<TimePeriodCategory, Guid>
    {
        Task<TimePeriodCategory> FindByNameAsync(string name);

        Task<List<TimePeriodCategory>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
            );
    }
}
