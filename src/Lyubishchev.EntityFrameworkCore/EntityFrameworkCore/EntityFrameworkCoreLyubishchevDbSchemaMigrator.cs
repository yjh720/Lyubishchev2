using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Lyubishchev.Data;
using Volo.Abp.DependencyInjection;

namespace Lyubishchev.EntityFrameworkCore;

public class EntityFrameworkCoreLyubishchevDbSchemaMigrator
    : ILyubishchevDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreLyubishchevDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the LyubishchevDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<LyubishchevDbContext>()
            .Database
            .MigrateAsync();
    }
}
