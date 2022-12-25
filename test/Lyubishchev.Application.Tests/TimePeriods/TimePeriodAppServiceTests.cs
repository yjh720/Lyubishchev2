using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Volo.Abp.Application.Dtos;
using Shouldly;
using System.Diagnostics.Contracts;
using Volo.Abp.Validation;
using Lyubishchev.TimePeriodCategories;

namespace Lyubishchev.TimePeriods
{
    public class TimePeriodAppServiceTests : LyubishchevApplicationTestBase
    {
        private readonly ITimePeriodAppService _timePeriodAppService;
        private readonly ITimePeriodCategoryAppService _timePeriodCategoryAppService;
        public TimePeriodAppServiceTests()
        {
            _timePeriodAppService = GetRequiredService<ITimePeriodAppService>();
            _timePeriodCategoryAppService = GetRequiredService<ITimePeriodCategoryAppService>();
        }

        [Fact]
        public async Task Should_Get_List_Of_TimePeriods()
        {
            //Act
            var result = await _timePeriodAppService.GetListAsync(
                new PagedAndSortedResultRequestDto()
                );

            //Assert
            result.TotalCount.ShouldBeGreaterThan(0);
            result.Items.ShouldContain(t => t.Name == "睡觉" &&
                                        t.CategoryName == "生活日常");
        }

        [Fact]
        public async Task Should_Create_A_Valid_TimePeriod()
        {
            var categories = await _timePeriodCategoryAppService.GetListAsync(new GetTimePeriodCategoryListDto());
            var firstCategory = categories.Items.First();

            //Act
            var result = await _timePeriodAppService.CreateAsync(
                new CreateUpdateTimePeriodDto
                {
                    Name = "New test timePeriod",
                    Start = DateTime.Now,
                    End = DateTime.Now.AddHours(1),
                    Note = "Test note",
                    CategoryId = firstCategory.Id
                }
                ) ;

            //Assert
            result.Id.ShouldNotBe(Guid.Empty);
            result.Name.ShouldBe("New test timePeriod");
        }

        [Fact]
        public async Task Should_Not_Create_A_TimePeriod_Without_Name()
        {
            var exception = await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _timePeriodAppService.CreateAsync(
                    new CreateUpdateTimePeriodDto
                    {
                        Name = "",
                        Start = DateTime.Now,
                        End = DateTime.Now.AddHours(1),
                        Note = ""
                    });
            });

            exception.ValidationErrors
                .ShouldContain(err => err.MemberNames.Any(mem => mem == "Name"));
        }
    }
}
