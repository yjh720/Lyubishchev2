using System;
using System.Collections.Generic;
using System.Text;
using Lyubishchev.Domain.TimePeriods;
using Lyubishchev.Localization;
using Lyubishchev.TimePeriods;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lyubishchev;

/* Inherit your application services from this class.
 */
public abstract class LyubishchevAppService : ApplicationService
{
    protected LyubishchevAppService()
    {
        LocalizationResource = typeof(LyubishchevResource);
    }
}
