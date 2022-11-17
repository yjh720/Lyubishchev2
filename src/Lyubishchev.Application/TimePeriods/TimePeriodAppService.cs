using Lyubishchev.Domain.TimePeriods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Lyubishchev.TimePeriods
{
    public class TimePeriodAppService : CrudAppService<
    TimePeriod,
    TimePeriodDto,
    Guid,
    PagedAndSortedResultRequestDto,
    CreateUpdateTimePeriodDto>, ITimePeriodAppService
    {
        public TimePeriodAppService(IRepository<TimePeriod, Guid> repository) : base(repository)
        {

        }
    }
}
