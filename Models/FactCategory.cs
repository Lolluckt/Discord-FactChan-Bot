using System.ComponentModel;

namespace DiscordFacts.Models;

public enum FactCategory
{
    [Description("science")]
    Science,

    [Description("history")]
    History,

    [Description("animals")]
    Animals,

    [Description("technology")]
    Technology,

    [Description("psychology")]
    Psychology,

    [Description("space")]
    Space,

    [Description("ocean")]
    Ocean,

    [Description("human body")]
    HumanBody,

    [Description("art")]
    Art,

    [Description("literature")]
    Literature
}
