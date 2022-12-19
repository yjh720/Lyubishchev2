using AutoMapper;
using Lyubishchev.TimePeriodCategories;
using Lyubishchev.TimePeriods;

namespace Lyubishchev.Blazor;

public class LyubishchevBlazorAutoMapperProfile : Profile
{
    public LyubishchevBlazorAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Blazor project.
        CreateMap<TimePeriodDto, CreateUpdateTimePeriodDto>();
        CreateMap<TimePeriodCategoryDto, UpdateTimePeriodCategoryDto>();
    }
}
