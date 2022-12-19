using Lyubishchev.Permissions;
using Lyubishchev.TimePeriodCategories;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Lyubishchev.TimePeriods
{
    [Authorize(LyubishchevPermissions.TimePeriodCategories.Default)]
    public class TimePeriodCategoryAppService : LyubishchevAppService, ITimePeriodCategoryAppService
    {
        private readonly ITimePeriodCategoryRepository _timePeriodCategoryRepository;
        private readonly TimePeriodCategoryManager _timePeriodCategoryManager;

        public TimePeriodCategoryAppService(
            ITimePeriodCategoryRepository timePeriodCategoryRepository,
            TimePeriodCategoryManager timePeriodCategoryManager)
        {
            _timePeriodCategoryManager = timePeriodCategoryManager;
            _timePeriodCategoryRepository = timePeriodCategoryRepository;
        }

        [Authorize(LyubishchevPermissions.TimePeriodCategories.Create)]
        public async Task<TimePeriodCategoryDto> CreateAsync(CreateTimePeriodCategoryDto input)
        {
            var timePeriodCategory = await _timePeriodCategoryManager.CreateAsync(
                input.Name,
                input.Description
                );

            await _timePeriodCategoryRepository.InsertAsync(timePeriodCategory);

            return ObjectMapper.Map<TimePeriodCategory, TimePeriodCategoryDto>(timePeriodCategory);
        }

        [Authorize(LyubishchevPermissions.TimePeriodCategories.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _timePeriodCategoryRepository.DeleteAsync(id);
        }

        public async Task<TimePeriodCategoryDto> GetAsync(Guid id)
        {
            var timePeriodCategory = await _timePeriodCategoryRepository.GetAsync(id);
            return ObjectMapper.Map<TimePeriodCategory, TimePeriodCategoryDto>(timePeriodCategory);
        }

        public async Task<PagedResultDto<TimePeriodCategoryDto>> GetListAsync(GetTimePeriodCategoryListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(TimePeriodCategory.Name);
            }

            var timePeriodCategories = await _timePeriodCategoryRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
                );

            var totalCount = input.Filter == null
                ? await _timePeriodCategoryRepository.CountAsync()
                : await _timePeriodCategoryRepository.CountAsync(
                    tc => tc.Name.Contains(input.Filter));

            return new PagedResultDto<TimePeriodCategoryDto>(
                totalCount,
                ObjectMapper.Map<List<TimePeriodCategory>, List<TimePeriodCategoryDto>>(timePeriodCategories)
                );
        }
        [Authorize(LyubishchevPermissions.TimePeriodCategories.Edit)]
        public async Task UpdateAsync(Guid id, UpdateTimePeriodCategoryDto input)
        {
            var timePeriodCategory = await _timePeriodCategoryRepository.GetAsync(id);

            if (timePeriodCategory.Name != input.Name)
            {
                await _timePeriodCategoryManager.ChangeNameAsync(timePeriodCategory, input.Name);
            }

            timePeriodCategory.Description = input.Description;

            await _timePeriodCategoryRepository.UpdateAsync(timePeriodCategory);
        }
    }
}
