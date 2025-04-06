namespace DiscordFacts.Localization
{
    public interface ILocalizationProvider
    {
        string GetString(string language, string key);
    }
}

