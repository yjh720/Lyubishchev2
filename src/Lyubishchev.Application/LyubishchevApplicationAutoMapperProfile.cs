using AutoMapper;
using JetBrains.Annotations;
using Lyubishchev.Domain.TimePeriods;
using Lyubishchev.TimePeriodCategories;
using Lyubishchev.TimePeriods;
using Volo.Abp.AutoMapper;

namespace Lyubishchev;

public class LyubishchevApplicationAutoMapperProfile : Profile
{
    public LyubishchevApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        //CreateMap<AppUser, AppUserDto>().Ignore(x => x.ExtraProperties);

        CreateMap<TimePeriod, TimePeriodDto>();
        CreateMap<CreateUpdateTimePeriodDto, TimePeriod>();

        CreateMap<TimePeriodCategory, TimePeriodCategoryDto>();
    }
}
