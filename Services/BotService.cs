using Discord;
using Discord.WebSocket;
using DiscordFacts.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DiscordFacts.Services;

public class BotService
{
    private readonly DiscordSocketClient _client;
    private readonly IConfiguration _config;
    private readonly ILogger<BotService> _logger;
    private readonly CommandHandler _commandHandler;
    private readonly FactScheduler _scheduler;

    public BotService(DiscordSocketClient client, IConfiguration config, ILogger<BotService> logger,
        CommandHandler commandHandler, FactScheduler scheduler)
    {
        _client = client;
        _config = config;
        _logger = logger;
        _commandHandler = commandHandler;
        _scheduler = scheduler;
    }

    public async Task StartAsync()
    {
        _client.Log += msg =>
        {
            _logger.LogInformation(msg.ToString());
            return Task.CompletedTask;
        };

        _client.Ready += async () =>
        {
            _logger.LogInformation("Bot is ready!");
            await _commandHandler.RegisterCommandsAsync();
            _ = _scheduler.StartSchedulerAsync();
        };

        _client.SlashCommandExecuted += _commandHandler.HandleCommandAsync;

        _client.ButtonExecuted += async component =>
        {
            var channelId = component.Channel.Id;

            try
            {
                await component.Message.DeleteAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete language prompt: {ex.Message}");
            }

            string reply = component.Data.CustomId switch
            {
                "lang_en" => "Language set to English!",
                "lang_ru" => "Язык установлен: русский!",
                _ => "Unknown selection."
            };

            _scheduler.SetLanguage(channelId, component.Data.CustomId == "lang_ru" ? "ru" : "en");
            var response = await component.Channel.SendMessageAsync(reply);

            _ = Task.Delay(TimeSpan.FromSeconds(5)).ContinueWith(async _ =>
            {
                try
                {
                    await response.DeleteAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Failed to delete language confirmation: {ex.Message}");
                }
            });
        };


        string token = _config["Discord:Token"];
        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();
    }
}
