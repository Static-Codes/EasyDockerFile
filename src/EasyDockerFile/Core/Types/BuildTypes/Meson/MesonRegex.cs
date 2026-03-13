using System.Text.RegularExpressions;

namespace EasyDockerFile.Core.Types.BuildTypes.Meson;

public static partial class MesonRegex
{
    [GeneratedRegex(
        pattern:
        @"project\(\s*'(?<project_name>.*)', " +
        @"'(?<project_language>.*)',\s*" +
        @"version : '(?<project_version>.*)',\s*" +
        @"meson_version : '(?<meson_version>.*)',\s*" +
        @"default_options : (?<build_arguments>\['.*'])", 
        options: RegexOptions.IgnoreCase,
        cultureName: "en-US"
    )]
    public static partial Regex MesonProjectObjectRegex { get; }

    [GeneratedRegex(@"'(?<val>.*?)'")]
    public static partial Regex GetArrayValueRegex { get; }

    [GeneratedRegex(@"project\s*\((?<content>.*?)\)", RegexOptions.Singleline)]
    public static partial Regex GetProjectBlock { get; }

    [GeneratedRegex(@"^\s*'(?<name>.*?)'\s*,\s*'(?<lang>.*?)'")]
    public static partial Regex GetProjectNameAndLanguage { get; }
}