using Lyubishchev.Migrations;
using Nito.AsyncEx;
using NSubstitute.Exceptions;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Lyubishchev.TimePeriodCategories
{
    public class TimePeriodCategoryAppService_Test : LyubishchevApplicationTestBase
    {
        private readonly ITimePeriodCategoryAppService _timePeriodCategoryAppService;

        public TimePeriodCategoryAppService_Test()
        {
            _timePeriodCategoryAppService = GetRequiredService<ITimePeriodCategoryAppService>();
        }

        [Fact]
        public async Task Should_Get_All_TimePeriodCategories_Without_Any_Filter()
        {
            var result = await _timePeriodCategoryAppService.GetListAsync(new GetTimePeriodCategoryListDto());

            result.TotalCount.ShouldBeGreaterThanOrEqualTo(2);
            result.Items.ShouldContain(tc => tc.Name == "生活日常");
            result.Items.ShouldContain(tc => tc.Name == "招聘");

        }

        [Fact]
        public async Task Should_Get_Filtered_TimePeriodCategories()
        {
            var result = await _timePeriodCategoryAppService.GetListAsync(
                new GetTimePeriodCategoryListDto { Filter = "生" });

            result.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
            result.Items.ShouldContain(tc => tc.Name == "生活日常");
            result.Items.ShouldNotContain(tc => tc.Name == "招聘");
        }

        [Fact]
        public async Task Should_Create_A_New_TimePeriodCategory()
        {
            var timePeriodCategoryDto = await _timePeriodCategoryAppService.CreateAsync(
                new CreateTimePeriodCategoryDto
                {
                    Name = "交通通勤",
                    Description = "交通通勤"
                }
                );

            timePeriodCategoryDto.Id.ShouldNotBe(Guid.Empty);
            timePeriodCategoryDto.Name.ShouldBe("交通通勤");
        }

        [Fact]
        public async Task Should_Not_Allow_To_Create_Duplicate_TimePeriodCategory()
        {
            await Assert.ThrowsAsync<TimePeriodCategoryAlreadyExistsException>(async () =>
            {
                await _timePeriodCategoryAppService.CreateAsync(
                    new CreateTimePeriodCategoryDto
                    {
                        Name = "生活日常",
                        Description = "生活日常"
                    });
            });
        }
    }
}
