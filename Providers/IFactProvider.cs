namespace DiscordFacts.Providers;

public interface IFactProvider
{
    Task<string> GetRandomFactAsync(string lang = "en");
    Task<string> GenerateWaifuMessageAsync();

}


