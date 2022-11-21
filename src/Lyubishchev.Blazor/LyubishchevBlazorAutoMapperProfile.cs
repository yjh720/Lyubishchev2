using AutoMapper;
using Lyubishchev.TimePeriods;

namespace Lyubishchev.Blazor;

public class LyubishchevBlazorAutoMapperProfile : Profile
{
    public LyubishchevBlazorAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Blazor project.
        CreateMap<TimePeriodDto, CreateUpdateTimePeriodDto>();
    }
}
