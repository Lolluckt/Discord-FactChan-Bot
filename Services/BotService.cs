using Discord;
using Discord.WebSocket;
using DiscordFacts.Commands;
using DiscordFacts.Localization;
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
    private readonly ILocalizationProvider _localizationProvider;

    public BotService(
        DiscordSocketClient client,
        IConfiguration config,
        ILogger<BotService> logger,
        CommandHandler commandHandler,
        FactScheduler scheduler,
        ILocalizationProvider localizationProvider)
    {
        _client = client;
        _config = config;
        _logger = logger;
        _commandHandler = commandHandler;
        _scheduler = scheduler;
        _localizationProvider = localizationProvider;
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

            string selectedLang = component.Data.CustomId switch
            {
                "lang_en" => "en",
                "lang_ru" => "ru",
                "lang_uk" => "uk",
                _ => "en"
            };

            string reply = _localizationProvider.GetString(selectedLang, "LanguageSet");

            _scheduler.SetLanguage(channelId, selectedLang);
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
