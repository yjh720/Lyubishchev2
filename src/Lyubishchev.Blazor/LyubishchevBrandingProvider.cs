using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Lyubishchev.Blazor;

[Dependency(ReplaceServices = true)]
public class LyubishchevBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Lyubishchev";
}
