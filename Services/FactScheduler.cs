using Discord;
using Discord.WebSocket;
using DiscordFacts.Providers;
using System.Collections.Concurrent;
using System.Text.Json;

namespace DiscordFacts.Services;

public class FactScheduler
{
    private readonly IFactProvider _factProvider;
    private readonly DiscordSocketClient _client;
    private readonly ConcurrentDictionary<ulong, string> _languagePrefs = new();
    private readonly List<ulong> _channels = new();
    private const string StateFile = "state.json";

    public FactScheduler(IFactProvider factProvider, DiscordSocketClient client)
    {
        _factProvider = factProvider;
        _client = client;

        LoadState();
    }

    public async Task StartSchedulerAsync()
    {
        while (true)
        {
            foreach (var channelId in _channels)
            {
                var channel = _client.GetChannel(channelId) as IMessageChannel;
                if (channel != null)
                {
                    var lang = _languagePrefs.GetValueOrDefault(channelId, "en");
                    var fact = await _factProvider.GetRandomFactAsync(lang);
                    await channel.SendMessageAsync(fact);
                }
            }


            await Task.Delay(TimeSpan.FromHours(1));

            // Проверка каждую минуту (для теста)
            //await Task.Delay(TimeSpan.FromMinutes(1));



        }
    }

    public void RegisterChannel(ulong channelId)
    {
        if (!_channels.Contains(channelId))
        {
            _channels.Add(channelId);
            SaveState();
        }
    }

    public void UnregisterChannel(ulong channelId)
    {
        if (_channels.Remove(channelId))
        {
            SaveState();
        }
    }

    public void SetLanguage(ulong channelId, string lang)
    {
        _languagePrefs[channelId] = lang;
        SaveState();
    }

    public string GetLanguage(ulong channelId)
    {
        return _languagePrefs.TryGetValue(channelId, out var lang) ? lang : "en";
    }

    public bool IsChannelRegistered(ulong channelId)
    {
        return _channels.Contains(channelId);
    }

    private void SaveState()
    {
        try
        {
            var state = new SchedulerState
            {
                Channels = _channels,
                Languages = _languagePrefs.ToDictionary(kvp => kvp.Key, kvp => kvp.Value)
            };

            var json = JsonSerializer.Serialize(state, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(StateFile, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save state: {ex.Message}");
        }
    }

    private void LoadState()
    {
        if (!File.Exists(StateFile)) return;

        try
        {
            var json = File.ReadAllText(StateFile);
            var state = JsonSerializer.Deserialize<SchedulerState>(json);

            if (state != null)
            {
                _channels.Clear();
                _channels.AddRange(state.Channels);

                _languagePrefs.Clear();
                foreach (var kvp in state.Languages)
                    _languagePrefs[kvp.Key] = kvp.Value;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load state: {ex.Message}");
        }
    }

    private class SchedulerState
    {
        public List<ulong> Channels { get; set; } = new();
        public Dictionary<ulong, string> Languages { get; set; } = new();
    }
}
