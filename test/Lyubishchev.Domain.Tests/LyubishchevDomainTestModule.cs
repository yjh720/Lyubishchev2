using Lyubishchev.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Lyubishchev;

[DependsOn(
    typeof(LyubishchevEntityFrameworkCoreTestModule)
    )]
public class LyubishchevDomainTestModule : AbpModule
{

}
