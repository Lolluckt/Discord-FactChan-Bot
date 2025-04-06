using DiscordFacts.Models;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace DiscordFacts.Providers;

public class FactProvider : IFactProvider
{
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient = new();

    public FactProvider(IConfiguration config)
    {
        _config = config;
    }

    public async Task<string> GetRandomFactAsync(string lang = "en")
    {
        var category = FactCategoryExtensions.GetRandomCategory();
        var categoryText = category.ToPromptString(lang);

        var prompt = lang == "ru"
            ? $"Расскажи короткий интересный факт о {categoryText}. Старайся ответить одним предложением."
            : $"Tell me a short and interesting fact about {categoryText}. Try answer in one sentence.";

        var body = new
        {
            model = "openchat/openchat-7b:free",
            messages = new[]
            {
                new { role = "user", content = prompt }
            }
        };

        var request = new HttpRequestMessage(HttpMethod.Post, "https://openrouter.ai/api/v1/chat/completions");
        request.Headers.Add("Authorization", $"Bearer {_config["OpenRouter:ApiKey"]}");
        request.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);
        var json = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            return $"❌ OpenRouter API error: {response.StatusCode}\n{json}";
        }

        try
        {
            using var doc = JsonDocument.Parse(json);
            var fact = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return $"#{categoryText} — {fact}";
        }
        catch (Exception ex)
        {
            return $"❌ Failed to parse LLM response: {ex.Message}\nRaw:\n{json}";
        }

    }
    public async Task<string> GenerateWaifuMessageAsync()
    {
        var prompt = "You're a cute anime waifu bot. Say something very encouraging, wholesome and uplifting in kawaii anime girl style. Tell them they are amazing, and make it sparkle~ Use emojis and sparkle energy~";

        var body = new
        {
            model = "openchat/openchat-7b:free",
            messages = new[]
            {
            new { role = "user", content = prompt }
        }
        };

        var request = new HttpRequestMessage(HttpMethod.Post, "https://openrouter.ai/api/v1/chat/completions");
        request.Headers.Add("Authorization", $"Bearer {_config["OpenRouter:ApiKey"]}");
        request.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);
        var json = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            return $"❌ API Error: {response.StatusCode}\n{json}";

        try
        {
            using var doc = JsonDocument.Parse(json);
            var reply = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            if (string.IsNullOrWhiteSpace(reply))
                return "Nyaa~ my waifu brain is empty... try again later~ (｡•́︿•̀｡)";
            var final = $"🌸 {reply}";
            if (final.Length > 2000)
                final = final.Substring(0, 1990) + "... 💫";

            return final;
        }
        catch (Exception ex)
        {
            return $"❌ Failed to parse waifu response: {ex.Message}\nRaw:\n{json}";
        }
    }


}
