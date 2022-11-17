using Lyubishchev.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Lyubishchev.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(LyubishchevEntityFrameworkCoreModule),
    typeof(LyubishchevApplicationContractsModule)
    )]
public class LyubishchevDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
