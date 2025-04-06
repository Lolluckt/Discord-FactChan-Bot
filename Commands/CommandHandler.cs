using Discord;
using Discord.WebSocket;
using DiscordFacts.Providers;
using DiscordFacts.Services;
using DiscordFacts.Localization;

namespace DiscordFacts.Commands;

public class CommandHandler
{
    private readonly DiscordSocketClient _client;
    private readonly FactScheduler _scheduler;
    private readonly IFactProvider _factProvider;
    private readonly ILocalizationProvider _localizationProvider;

    public CommandHandler(DiscordSocketClient client, FactScheduler scheduler, IFactProvider factProvider, ILocalizationProvider localizationProvider)
    {
        _client = client;
        _scheduler = scheduler;
        _factProvider = factProvider;
        _localizationProvider = localizationProvider;
    }

    public async Task RegisterCommandsAsync()
    {
        var start = new SlashCommandBuilder()
            .WithName("start")
            .WithDescription("Start posting facts in this channel");

        var stop = new SlashCommandBuilder()
            .WithName("stop")
            .WithDescription("Stop posting facts in this channel");

        var language = new SlashCommandBuilder()
            .WithName("language")
            .WithDescription("Choose language for facts");

        var nextFact = new SlashCommandBuilder()
            .WithName("nextfact")
            .WithDescription("Get a random fact immediately");

        var ping = new SlashCommandBuilder()
            .WithName("ping")
            .WithDescription("Check if bot is online");

        var help = new SlashCommandBuilder()
            .WithName("help")
            .WithDescription("Show available commands");

        var info = new SlashCommandBuilder()
            .WithName("info")
            .WithDescription("About this bot");

        var hug = new SlashCommandBuilder()
            .WithName("hug")
            .WithDescription("Ask for a hug (if you're nice~)");

        var senpai = new SlashCommandBuilder()
            .WithName("senpai")
            .WithDescription("Get support from anime waifu bot~");

        await _client.CreateGlobalApplicationCommandAsync(start.Build());
        await _client.CreateGlobalApplicationCommandAsync(stop.Build());
        await _client.CreateGlobalApplicationCommandAsync(language.Build());
        await _client.CreateGlobalApplicationCommandAsync(nextFact.Build());
        await _client.CreateGlobalApplicationCommandAsync(ping.Build());
        await _client.CreateGlobalApplicationCommandAsync(help.Build());
        await _client.CreateGlobalApplicationCommandAsync(info.Build());
        await _client.CreateGlobalApplicationCommandAsync(hug.Build());
        await _client.CreateGlobalApplicationCommandAsync(senpai.Build());
    }

    public async Task HandleCommandAsync(SocketSlashCommand command)
    {
        var channelId = command.Channel.Id;
        var lang = _scheduler.GetLanguage(channelId);

        switch (command.CommandName)
        {
            case "start":
                if (_scheduler.IsChannelRegistered(channelId))
                {
                    await command.RespondAsync(_localizationProvider.GetString(lang, "StartAlreadyRegistered"));
                }
                else
                {
                    _scheduler.RegisterChannel(channelId);
                    await command.RespondAsync(_localizationProvider.GetString(lang, "StartRegistered"));
                }
                break;

            case "stop":
                _scheduler.UnregisterChannel(channelId);
                await command.RespondAsync(_localizationProvider.GetString(lang, "Stop"));
                break;

            case "language":
                var builder = new ComponentBuilder()
                    .WithButton("🇬🇧 English", "lang_en", ButtonStyle.Primary)
                    .WithButton("🇷🇺 Russian", "lang_ru", ButtonStyle.Secondary)
                    .WithButton("🇺🇦 Українська", "lang_uk", ButtonStyle.Secondary);

                await command.RespondAsync(_localizationProvider.GetString(lang, "ChooseLanguage"), components: builder.Build());
                break;

            case "nextfact":
                await command.RespondAsync(_localizationProvider.GetString(lang, "NextFactIntro"), ephemeral: false);
                var fact = await _factProvider.GetRandomFactAsync(lang);
                await command.Channel.SendMessageAsync(fact);
                break;

            case "ping":
                await command.RespondAsync(_localizationProvider.GetString(lang, "Ping"));
                break;

            case "help":
                await command.RespondAsync(_localizationProvider.GetString(lang, "Help"));
                break;

            case "info":
                await command.RespondAsync(_localizationProvider.GetString(lang, "Info"));
                break;

            case "hug":
                var user = command.User.Username;
                var hugMsg = string.Format(_localizationProvider.GetString(lang, "Hug"), user);
                await command.RespondAsync(hugMsg);
                break;

            case "senpai":
                var introMessage = _localizationProvider.GetString(lang, "SenpaiIntro");
                await command.RespondAsync(introMessage);
                var encouragement = await _factProvider.GenerateWaifuMessageAsync(lang);
                string safeReply = encouragement.Length > 2000
                    ? encouragement[..1990] + "..."
                    : encouragement;
                await command.ModifyOriginalResponseAsync(x => x.Content = safeReply);
                break;
        }
    }
}
