using System.ComponentModel;
using System.Reflection;

namespace DiscordFacts.Models;

public static class FactCategoryExtensions
{
    private static readonly Random _random = new();

    public static FactCategory GetRandomCategory()
    {
        var values = Enum.GetValues(typeof(FactCategory)).Cast<FactCategory>().ToArray();
        return values[_random.Next(values.Length)];
    }

    public static string ToPromptString(this FactCategory category, string lang = "en")
    {
        return lang switch
        {
            "ru" => category switch
            {
                FactCategory.Science => "наука",
                FactCategory.History => "история",
                FactCategory.Animals => "животные",
                FactCategory.Technology => "технологии",
                FactCategory.Psychology => "психология",
                FactCategory.Space => "космос",
                FactCategory.Ocean => "океан",
                FactCategory.HumanBody => "человеческое тело",
                FactCategory.Art => "искусство",
                FactCategory.Literature => "литература",
                _ => category.ToString()
            },
            "uk" => category switch
            {
                FactCategory.Science => "наука",
                FactCategory.History => "історія",
                FactCategory.Animals => "тварини",
                FactCategory.Technology => "технології",
                FactCategory.Psychology => "психологія",
                FactCategory.Space => "космос",
                FactCategory.Ocean => "океан",
                FactCategory.HumanBody => "людське тіло",
                FactCategory.Art => "мистецтво",
                FactCategory.Literature => "література",
                _ => category.ToString()
            },
            _ => category.GetEnumDescription()
        };
    }

    private static string GetEnumDescription(this Enum value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());
        var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }
}
