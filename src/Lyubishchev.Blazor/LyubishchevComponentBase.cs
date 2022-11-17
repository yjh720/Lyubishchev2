using Lyubishchev.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Lyubishchev.Blazor;

public abstract class LyubishchevComponentBase : AbpComponentBase
{
    protected LyubishchevComponentBase()
    {
        LocalizationResource = typeof(LyubishchevResource);
    }
}
