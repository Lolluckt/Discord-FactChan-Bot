using Discord;
using Discord.WebSocket;
using DiscordFacts.Commands;
using DiscordFacts.Providers;
using DiscordFacts.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config =>
    {
        config.AddJsonFile("appsettings.json");
        config.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton<DiscordSocketClient>();
        services.AddSingleton<CommandHandler>();
        services.AddSingleton<BotService>();
        services.AddSingleton<IFactProvider, FactProvider>();
        services.AddSingleton<FactScheduler>();
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    })
    .Build();



var bot = host.Services.GetRequiredService<BotService>();
await bot.StartAsync();

await host.RunAsync();
