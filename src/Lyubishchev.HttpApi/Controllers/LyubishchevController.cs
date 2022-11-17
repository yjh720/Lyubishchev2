using Lyubishchev.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Lyubishchev.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class LyubishchevController : AbpControllerBase
{
    protected LyubishchevController()
    {
        LocalizationResource = typeof(LyubishchevResource);
    }
}
