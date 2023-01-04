using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;

using Radzen;
using Microsoft.Extensions.DependencyInjection;

namespace Lyubishchev.Blazor;

public class Program
{
    public async static Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        //radzen
        builder.Services.AddScoped<DialogService>();

        var application = await builder.AddApplicationAsync<LyubishchevBlazorModule>(options =>
        {
            options.UseAutofac();
        });

        var host = builder.Build();

        await application.InitializeApplicationAsync(host.Services);

        await host.RunAsync();
    }
}
