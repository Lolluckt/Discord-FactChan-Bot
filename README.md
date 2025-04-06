# FactsBot-chan

A Discord bot that automatically sends interesting facts generated by AI to a designated channel every hour.

## Features

- Sends random fun facts every hour in a text channel
- `/start` and `/stop` commands for channel binding
- Multilingual support now work only for every hour fact (English and Russian) with `/language`
- `/nextfact` command for immediate fact request
- `/senpai` — anime-style kawaii encouragement (always unique thanks to AI)
- `/hug` — sends a cute virtual hug with your name
- `/help` — list of available commands
- `/info` — bot information in character style
- Saves bound channels to persist after bot restarts

## Technologies

- C# .NET 8
- Discord.Net
- Dependency Injection (Microsoft.Extensions)
- OpenRouter API for LLM text generation

## Setup

Clone the repository:
   ```bash
   git clone https://github.com/Lolluckt/Discord-FactChan-Bot.git
   cd Discord-FactChan-Bot
   ```
---

## Required Bot Permissions

To make the bot work properly, grant the following in the OAuth2 settings while creating bot on discord dev portal:

### Scopes:
- `bot`
- `applications.commands`

### Bot Permissions:
- `Administrator` (recommended for testing)
  - OR selectively enable:
    - `Send Messages`
    - `Use Slash Commands`
    - `Read Message History`
    - `Manage Messages` (optional for ephemeral cleanup)
    - `Embed Links`

---
   
## Configuration

First you need to create a file named `appsettings.json` in the root of the project:

```json
{
  "Discord": {
    "Token": "DISCORD_BOT_TOKEN"
  },
  "OpenRouter": {
    "ApiKey": "OPENROUTER_API_KEY"
  }
}
```
Run the bot:
   ```bash
   dotnet run
   ```

## Commands

| Command      | Description |
|--------------|-------------|
| `/start`     | Start hourly fact posting |
| `/stop`      | Stop posting |
| `/language`  | Select the language via buttons |
| `/nextfact`  | Get an instant fact |
| `/senpai`    | Receive cute encouragement |
| `/hug`       | Virtual hug with your name |
| `/ping`      | Check if bot is online |
| `/help`      | Display available commands |
| `/info`      | Bot introduction |

## Notes

- Free OpenRouter models like `openchat-7b:free` are used
- Time intervals can be adjusted in `FactScheduler.cs`
- App remembers which channels to post to across sessions via JSON (look for state.json)

---

## License

Feel free to use
