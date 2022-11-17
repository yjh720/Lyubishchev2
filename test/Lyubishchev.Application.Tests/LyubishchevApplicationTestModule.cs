using Volo.Abp.Modularity;

namespace Lyubishchev;

[DependsOn(
    typeof(LyubishchevApplicationModule),
    typeof(LyubishchevDomainTestModule)
    )]
public class LyubishchevApplicationTestModule : AbpModule
{

}
