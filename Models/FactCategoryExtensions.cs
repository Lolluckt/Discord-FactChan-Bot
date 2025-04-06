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
                FactCategory.Science => "науке",
                FactCategory.History => "истории",
                FactCategory.Animals => "животных",
                FactCategory.Technology => "технологиях",
                FactCategory.Psychology => "психологии",
                FactCategory.Space => "космосе",
                FactCategory.Ocean => "океане",
                FactCategory.HumanBody => "человеческом теле",
                FactCategory.Art => "искусстве",
                FactCategory.Literature => "литературе",
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
