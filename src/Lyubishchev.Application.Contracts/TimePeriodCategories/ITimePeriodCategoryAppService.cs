using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lyubishchev.TimePeriodCategories
{
    public interface ITimePeriodCategoryAppService : IApplicationService
    {
        Task<TimePeriodCategoryDto> GetAsync(Guid id);
        Task<PagedResultDto<TimePeriodCategoryDto>> GetListAsync(GetTimePeriodCategoryListDto input);
        Task<TimePeriodCategoryDto> CreateAsync(CreateTimePeriodCategoryDto input);
        Task UpdateAsync(Guid id, UpdateTimePeriodCategoryDto input);
        Task DeleteAsync(Guid id);
    }
}
