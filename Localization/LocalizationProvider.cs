using System.Collections.Generic;

namespace DiscordFacts.Localization
{
    public class LocalizationProvider : ILocalizationProvider
    {
        private readonly Dictionary<string, Dictionary<string, string>> _resources = new()
        {
            ["en"] = new Dictionary<string, string>
            {
                ["FactPrompt"] = "Tell me a short and interesting fact about {0}. Try answer in one sentence.",
                ["LanguageSet"] = "Language set to English!",
                ["ChooseLanguage"] = "Choose your language, senpai~:",
                ["StartAlreadyRegistered"] = "AAAH? YOU SCARED ME! Nyaa~! I already started sending facts here! Don't be so impatient~ (≧◡≦)",
                ["StartRegistered"] = "Yatta~! I'm now sending random facts every hour in this channel! (⁄ ⁄>⁄ ▽ ⁄<⁄ ⁄)",
                ["Stop"] = "Aww... okay! I'll stop for now. Let me know when you need me again~ (｡•́︿•̀｡)",
                ["NextFactIntro"] = "Let me think... nya~ here's something you might like! (๑˃ᴗ˂)ﻭ",
                ["Ping"] = "Hey, dont touch me~~! I'm totally awake and ready~ (≧ω≦)",
                ["Help"] = "**Available Commands, nya~!**\n`/start` — Start hourly fact delivery~ \n`/stop` — Stop sending facts... (T_T)\n`/language` — Choose your language~ \n`/nextfact` — Gimme one fact right meow! \n`/ping` — Are you there, bot-senpai? \n`/info` — What even am I? \n`/hug` — Ask me for a hug~ uwu 💕\n`/senpai` — I’ll cheer you up like a proper anime waifu~ \n\n*Use them wisely, okay~? (≧◡≦)*",
                ["Info"] = " *Hehe~ Konbanwa!* I'm **FactsBot-chan** — your cozy little assistant for sharing fun random facts!\n\nI pop in every hour to share wisdom, history, science, or even space stuff~ \nJust say `/start` and I'll sparkle my way into your channel~ \n\n*Let's get smarter together, one fact at a time~ nyaa~!* (ฅ'ω'ฅ)",
                ["Hug"] = "Mou~ I'll hug you, {0}-chan~ (つ≧▽≦)つ 💞",
                ["SenpaiIntro"] = "One moment~ charging my kawaii energy~ (￣︶￣)",
                ["SenpaiPrompt"] =
                    "You are a cute anime waifu and play role of it. Write uplifting, and wholesome message in English and in anime style. Use Japanese-style kaomoji (instead emojis). DONT USE EMOJIS"
            },
            ["ru"] = new Dictionary<string, string>
            {
                ["FactPrompt"] = "Расскажи короткий интересный факт о {0}. Старайся ответить одним предложением.",
                ["LanguageSet"] = "Язык установлен: русский!",
                ["ChooseLanguage"] = "Выберите язык, сенпай~:",
                ["StartAlreadyRegistered"] = "AAAH? ТЫ МЕНЯ ИСПУГАЛ! Nyaa~! Я уже начала отправлять факты в этот канал! Не будь таким нетерпеливым~ (≧◡≦)",
                ["StartRegistered"] = "Yatta~! Теперь я буду отправлять случайные факты каждый час в этом канале! (⁄ ⁄>⁄ ▽ ⁄<⁄ ⁄)",
                ["Stop"] = "Ну ладно... Я остановлюсь. Дай знать, когда снова понадоблюсь~ (｡•́︿•̀｡)",
                ["NextFactIntro"] = "Дай подумать... nya~ вот что тебе может понравиться! (๑˃ᴗ˂)ﻭ",
                ["Ping"] = "Эй, не трогай меня~~! Я полностью готова~ (≧ω≦)",
                ["Help"] = "**Доступные команды, nya~!**\n`/start` — Начать отправку фактов каждый час~ \n`/stop` — Прекратить отправку фактов... (T_T)\n`/language` — Выбрать язык~ \n`/nextfact` — Дай сразу один факт! \n`/ping` — Ты там, бот-чан? \n`/info` — Что я вообще? \n`/hug` — Обними меня~ uwu 💕\n`/senpai` — Я подбодрю тебя, как настоящая аниме-вайфу~ \n\n*Пользуйтесь с умом, хорошо~? (≧◡≦)*",
                ["Info"] = " *Хи-хи~ Konbanwa!* Я **FactsBot-chan** — твоя уютная помощница, которая делится забавными фактами!\n\nЯ каждый час делюсь мудростью, историей, наукой или даже космическими фактами~ \nПросто напиши `/start`, и я зайду в твой канал~ \n\n*Давайте становиться умнее вместе, факт за фактом~ nyaa~!* (ฅ'ω'ฅ)",
                ["Hug"] = "Мяу~ Обнимаю тебя, {0}-chan~ (つ≧▽≦)つ 💞",
                ["SenpaiIntro"] = "Минуточку~ заряжаю свою кавайную энергию~ (￣︶￣)",
                ["SenpaiPrompt"] =
                    "Ты милая аниме-вайфу играй ее роль. Напиши приподнимающее и милое настроение сообщение на русском языке в аниме стиле. Используй японские каомодзи (Эмодзи НЕЛЬЗЯ)."

            },
            ["uk"] = new Dictionary<string, string>
            {
                ["FactPrompt"] = "Розкажи короткий цікавий факт про {0}. Намагайся відповісти одним реченням.",
                ["LanguageSet"] = "Мова встановлена: українська!",
                ["ChooseLanguage"] = "Оберіть мову, сенпай~:",
                ["StartAlreadyRegistered"] = "AAAH? ТИ МЕНЕ ЗЛЯКАВ! Nyaa~! Я вже почала надсилати факти в цей канал! Не поспішай~ (≧◡≦)",
                ["StartRegistered"] = "Yatta~! Тепер я надсилатиму випадкові факти кожну годину в цьому каналі! (⁄ ⁄>⁄ ▽ ⁄<⁄ ⁄)",
                ["Stop"] = "Добре... Я зупинюся. Повідом, коли знову буду потрібна~ (｡•́︿•̀｡)",
                ["NextFactIntro"] = "Дай подумати... nya~ ось щось, що може тобі сподобатися! (๑˃ᴗ˂)ﻭ",
                ["Ping"] = "Гей, не чіпай мене~~! Я повністю готова~ (≧ω≦)",
                ["Help"] = "**Доступні команди, nya~!**\n`/start` — Почати надсилання фактів кожну годину~ \n`/stop` — Зупинити надсилання фактів... (T_T)\n`/language` — Обрати мову~ \n`/nextfact` — Одразу дай один факт! \n`/ping` — Ти там, бот-чан? \n`/info` — Що я таке? \n`/hug` — Обійми мене~ uwu 💕\n`/senpai` — Я підбадьорю тебе, як справжня аніме-вайфу~ \n\n*Використовуй їх розумно, гаразд~? (≧◡≦)*",
                ["Info"] = " *Хе-хе~ Konbanwa!* Я **FactsBot-chan** — твоя затишна помічниця для надсилання веселих та цікавих випадкових фактів!\n\nЯ щогодини надсилаю мудрість, історію, науку чи навіть космічні факти~ \nПросто напиши `/start`, і я з’явлюся в твоєму каналі~ \n\n*Ставаймо розумнішими разом, факт за фактом~ nyaa~!* (ฅ'ω'ฅ)",
                ["Hug"] = "Мяу~ Обійму тебе, {0}-chan~ (つ≧▽≦)つ 💞",
                ["SenpaiIntro"] = "Хвилиночку~ заряджаю свою кавайну енергію~ (￣︶￣)",
                ["SenpaiPrompt"] =
                    "Ти мила аніме-вайфу грай її роль. Напиши підбадьорливе та миле повідомлення українською мовою в аніме стилі. Використовуй японські каомоджі (замість емодзі)."

            }
        };

        public string GetString(string language, string key)
        {
            if (_resources.TryGetValue(language, out var langDict))
            {
                if (langDict.TryGetValue(key, out var value))
                {
                    return value;
                }
            }
            if (_resources.TryGetValue("en", out var fallbackDict) && fallbackDict.TryGetValue(key, out var fallbackValue))
            {
                return fallbackValue;
            }
            return $"[[{key}]]";
        }
    }
}
