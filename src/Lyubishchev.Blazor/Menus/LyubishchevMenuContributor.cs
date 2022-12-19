using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Lyubishchev.Localization;
using Volo.Abp.Account.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;
using System.ComponentModel.DataAnnotations;
using Lyubishchev.Permissions;

namespace Lyubishchev.Blazor.Menus;

public class LyubishchevMenuContributor : IMenuContributor
{
    private readonly IConfiguration _configuration;

    public LyubishchevMenuContributor(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
        else if (context.Menu.Name == StandardMenus.User)
        {
            await ConfigureUserMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<LyubishchevResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                LyubishchevMenus.Home,
                l["Menu:Home"],
                "/",
                icon: "fas fa-home"
            )
        );

        var lyubishchevMenu = new ApplicationMenuItem(
            "Lyubishchev",
            l["Menu:Lyubishchev"],
            icon: "");

        lyubishchevMenu.AddItem(
            new ApplicationMenuItem(
                "Lyubishchev.TimePeriods",
                l["Menu:TimePeriods"],
                url: "/timePeriods"));

        //context.Menu.AddItem(
        //    new ApplicationMenuItem(
        //        "Lyubishchev",
        //        l["Menu:Lyubishchev"],
        //        icon: ""
        //        ).AddItem(
        //        new ApplicationMenuItem(
        //        "Lyubishchev.TimePeriods",
        //        l["Menu:TimePeriods"],
        //        url: "/timePeriods")));

        if (await context.IsGrantedAsync(LyubishchevPermissions.TimePeriodCategories.Default))
        {
            //context.Menu.AddItem(new ApplicationMenuItem(
            //    "Lyubishchev.TimePeriodCategory",
            //    l["Menu:TimePeriodCategories"],
            //    url: "/timePeriodCategories"
            //    ));
            lyubishchevMenu.AddItem(new ApplicationMenuItem(
                "Lyubishchev.TimePeriodCategory",
                l["Menu:TimePeriodCategories"],
                url: "/timePeriodCategories"
                ));
        }

        context.Menu.AddItem(lyubishchevMenu);
    }

    private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
    {
        var accountStringLocalizer = context.GetLocalizer<AccountResource>();

        var authServerUrl = _configuration["AuthServer:Authority"] ?? "";

        context.Menu.AddItem(new ApplicationMenuItem(
            "Account.Manage",
            accountStringLocalizer["MyAccount"],
            $"{authServerUrl.EnsureEndsWith('/')}Account/Manage?returnUrl={_configuration["App:SelfUrl"]}",
            icon: "fa fa-cog",
            order: 1000,
            null).RequireAuthenticated());

        return Task.CompletedTask;
    }
}
