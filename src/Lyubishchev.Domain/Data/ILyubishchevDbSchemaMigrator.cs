using System.Threading.Tasks;

namespace Lyubishchev.Data;

public interface ILyubishchevDbSchemaMigrator
{
    Task MigrateAsync();
}
