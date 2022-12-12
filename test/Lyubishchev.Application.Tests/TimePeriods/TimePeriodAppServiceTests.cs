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

namespace Lyubishchev.TimePeriods
{
    public class TimePeriodAppServiceTests : LyubishchevApplicationTestBase
    {
        private readonly ITimePeriodAppService _timePeriodAppService;
        public TimePeriodAppServiceTests()
        {
            _timePeriodAppService = GetRequiredService<ITimePeriodAppService>();
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
            result.Items.ShouldContain(t => t.Name == "睡觉");
        }

        [Fact]
        public async Task Should_Create_A_Valid_TimePeriod()
        {
            //Act
            var result = await _timePeriodAppService.CreateAsync(
                new CreateUpdateTimePeriodDto
                {
                    Name = "New test timePeriod",
                    Start = DateTime.Now,
                    End = DateTime.Now.AddHours(1),
                    Note = "Test note"
                }
                );

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
