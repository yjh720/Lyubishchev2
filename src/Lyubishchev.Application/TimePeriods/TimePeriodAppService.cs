using Lyubishchev.Domain.TimePeriods;
using Lyubishchev.Permissions;
using Lyubishchev.TimePeriodCategories;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Lyubishchev.TimePeriods
{
    [Authorize(LyubishchevPermissions.TimePeriods.Default)]
    public class TimePeriodAppService : CrudAppService<
    TimePeriod,
    TimePeriodDto,
    Guid,
    PagedAndSortedResultRequestDto,
    CreateUpdateTimePeriodDto>, ITimePeriodAppService
    {
        private readonly ITimePeriodCategoryRepository _timePeriodCategoryRepository;
        public TimePeriodAppService(IRepository<TimePeriod, Guid> repository,
            ITimePeriodCategoryRepository timePeriodCategoryRepository) : base(repository)
        {
            _timePeriodCategoryRepository = timePeriodCategoryRepository;
            GetPolicyName = LyubishchevPermissions.TimePeriods.Default;
            GetListPolicyName = LyubishchevPermissions.TimePeriods.Default;
            CreatePolicyName = LyubishchevPermissions.TimePeriods.Create;
            UpdatePolicyName = LyubishchevPermissions.TimePeriods.Edit;
            DeletePolicyName = LyubishchevPermissions.TimePeriods.Delete;
        }

        public override async Task<TimePeriodDto> GetAsync(Guid id)
        {
            //Get the IQueryable<TimePeriod> from the repository
            var queryable = await Repository.GetQueryableAsync();

            //Prepare a query to join timePeriods and categories
            var query = from timePeriod in queryable
                        join category in await _timePeriodCategoryRepository.GetQueryableAsync() on timePeriod.CategoryId equals category.Id
                        where timePeriod.Id == id
                        select new {timePeriod, category};

            //Execute the query and get the timePeriod with categories
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(TimePeriod), id);
            }

            var timePeriodDto = ObjectMapper.Map<TimePeriod, TimePeriodDto>(queryResult.timePeriod);
            timePeriodDto.CategoryName = queryResult.category.Name;
            return timePeriodDto;
        }

        public override async Task<PagedResultDto<TimePeriodDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            //Get the IQueryable<TimePeriod> from the repository
            var queryable = await Repository.GetQueryableAsync();

            //Prepare a query to join timperiods and categories
            var query = from timePeriod in queryable
                        join category in await _timePeriodCategoryRepository.GetQueryableAsync() on timePeriod.CategoryId equals category.Id
                        select new {timePeriod, category};

            //Paging
            query = query
                .OrderBy(NormalizeSorting(input.Sorting))
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of TimePeriodDto objects
            var timePeriodDtos = queryResult.Select(x =>
            {
                var timePeriodDto = ObjectMapper.Map<TimePeriod, TimePeriodDto>(x.timePeriod);
                timePeriodDto.CategoryName = x.category.Name;
                return timePeriodDto;
            }).ToList();

            //Get the total count with another query
            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<TimePeriodDto>(
                totalCount,
                timePeriodDtos
                );
        }

        public async Task<ListResultDto<TimePeriodCategoryLookupDto>> GetTimePeriodCategoryLookupAsync()
        {
            var timePeriodCategories = await _timePeriodCategoryRepository.GetListAsync();

            return new ListResultDto<TimePeriodCategoryLookupDto>(
                ObjectMapper.Map<List<TimePeriodCategory>, List<TimePeriodCategoryLookupDto>>(timePeriodCategories)
                );
        }

        private static string NormalizeSorting(string sorting)
        {
            if (sorting.IsNullOrEmpty())
            {
                return $"timePeriod.{nameof(TimePeriod.Name)}";
            }

            if (sorting.Contains("timePeriodCategoryName", StringComparison.OrdinalIgnoreCase))
            {
                return sorting.Replace(
                    "timePeriodCategoryName",
                    "timePeriodCategory.Name",
                    StringComparison.OrdinalIgnoreCase
                    );
            }

            return $"timePeriod.{sorting}";
        }
    }
}
