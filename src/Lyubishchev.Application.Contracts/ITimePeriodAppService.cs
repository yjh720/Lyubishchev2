using Lyubishchev.TimePeriods;
using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
