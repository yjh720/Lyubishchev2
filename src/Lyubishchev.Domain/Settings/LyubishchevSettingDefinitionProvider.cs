using Volo.Abp.Settings;

namespace Lyubishchev.Settings;

public class LyubishchevSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(LyubishchevSettings.MySetting1));
    }
}
