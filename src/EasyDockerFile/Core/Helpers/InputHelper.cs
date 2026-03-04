using Spectre.Console;

namespace EasyDockerFile.Core.Helpers;


public static class InputHelper 
{
    public static string AskForInput(string message, string[] options)
    {
        var style = new Style(decoration: Decoration.Bold);
        var prompt = new SelectionPrompt<string>() { SearchEnabled = true }
        .HighlightStyle(style)
        .Title(message)
        .AddChoices(
            options.Select(
                opt => opt.EscapeMarkup()
            )
        )
        .PageSize(Math.Max(options.Length, 3));
        return AnsiConsole.Prompt(prompt);
    }

    public static string AskForInput(string message, IEnumerable<string> options)
    {
        var style = new Style(decoration: Decoration.Bold);
        var prompt = new SelectionPrompt<string>() { SearchEnabled = true }
        .HighlightStyle(style)
        .Title(message)
        .AddChoices(
            options.Select(
                opt => opt.EscapeMarkup()
            )
        )
        .PageSize(Math.Max(options.Count(), 3));
        return AnsiConsole.Prompt(prompt);
    }
}