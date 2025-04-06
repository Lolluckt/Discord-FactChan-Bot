using Discord;
using Discord.WebSocket;
using DiscordFacts.Providers;
using DiscordFacts.Services;

namespace DiscordFacts.Commands;

public class CommandHandler
{
    private readonly DiscordSocketClient _client;
    private readonly FactScheduler _scheduler;
    private readonly IFactProvider _factProvider;

    public CommandHandler(DiscordSocketClient client, FactScheduler scheduler, IFactProvider factProvider)
    {
        _client = client;
        _scheduler = scheduler;
        _factProvider = factProvider;
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

        switch (command.CommandName)
        {
            case "start":
                if (_scheduler.IsChannelRegistered(channelId))
                {
                    await command.RespondAsync("AAAH? YOU SCARED ME! Nyaa~! I already started sending facts here! Don't be so impatient~ (≧◡≦)");
                }
                else
                {
                    _scheduler.RegisterChannel(channelId);
                    await command.RespondAsync("Yatta~! I'm now sending random facts every hour in this channel! (⁄ ⁄>⁄ ▽ ⁄<⁄ ⁄)");
                }
                break;

            case "stop":
                _scheduler.UnregisterChannel(channelId);
                await command.RespondAsync("Aww... okay! I'll stop for now. Let me know when you need me again~ (｡•́︿•̀｡)");
                break;

            case "language":
                var builder = new ComponentBuilder()
                    .WithButton("🇬🇧 English", "lang_en", ButtonStyle.Primary)
                    .WithButton("🇷🇺 Russian", "lang_ru", ButtonStyle.Secondary);

                await command.RespondAsync("Choose your language, senpai~:", components: builder.Build());
                break;

            case "nextfact":
                var lang = _scheduler.GetLanguage(channelId);
                await command.RespondAsync("Let me think... nya~ here's something you might like! (๑˃ᴗ˂)ﻭ", ephemeral: false);

                var fact = await _factProvider.GetRandomFactAsync(lang);
                await command.Channel.SendMessageAsync(fact);
                break;

            case "ping":
                await command.RespondAsync("Hey, dont touch me~~! I'm totally awake and ready~ (≧ω≦)");
                break;

            case "help":
                await command.RespondAsync("**Available Commands, nya~!**\n" +
                    "`/start` — Start hourly fact delivery~ \n" +
                    "`/stop` — Stop sending facts... (T_T)\n" +
                    "`/language` — Choose your language~ \n" +
                    "`/nextfact` — Gimme one fact right meow! \n" +
                    "`/ping` — Are you there, bot-senpai? \n" +
                    "`/info` — What even am I? \n" +
                    "`/hug` — Ask me for a hug~ uwu 💕\n" +
                    "`/senpai` — I’ll cheer you up like a proper anime waifu~ \n\n" +
                    "*Use them wisely, okay~? (≧◡≦)*");
                break;

            case "info":
                await command.RespondAsync(
                    " *Hehe~ Konbanwa!* I'm **FactsBot-chan** — your cozy little assistant for sharing fun random facts!\n\n" +
                    "I pop in every hour to share wisdom, history, science, or even space stuff~ \n" +
                    "Just say `/start` and I'll sparkle my way into your channel~ \n\n" +
                    "*Let's get smarter together, one fact at a time~ nyaa~!* (ฅ'ω'ฅ)"
                );
                break;

            case "hug":
                var user = command.User.Username;
                await command.RespondAsync($"Mou~ I'll hug you, {user}-chan~ (つ≧▽≦)つ 💞");
                break;

            case "senpai":
                await command.RespondAsync("One moment~ charging my kawaii energy~ (￣︶￣)");

                var encouragement = await _factProvider.GenerateWaifuMessageAsync();

                // Обрезаем до безопасной длины
                string safeReply = encouragement.Length > 2000
                    ? encouragement.Substring(0, 1990) + "..." // оставляем запас под emoji и символы
                    : encouragement;

                await command.FollowupAsync(safeReply);
                break;


        }
    }
}
