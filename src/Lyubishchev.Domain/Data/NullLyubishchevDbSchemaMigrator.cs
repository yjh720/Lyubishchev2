using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Lyubishchev.Data;

/* This is used if database provider does't define
 * ILyubishchevDbSchemaMigrator implementation.
 */
public class NullLyubishchevDbSchemaMigrator : ILyubishchevDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
