using Lyubishchev.TimePeriods;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lyubishchev
{
    public interface ITimePeriodAppService:
        ICrudAppService<
            TimePeriodDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateTimePeriodDto>
    {
        // add the new method
        Task<ListResultDto<TimePeriodCategoryLookupDto>> GetTimePeriodCategoryLookupAsync();
    }
}
