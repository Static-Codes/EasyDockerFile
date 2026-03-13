using System.Text.RegularExpressions;

namespace EasyDockerFile.Core.Types.BuildTypes.Meson;

public static partial class MesonRegex
{
    [GeneratedRegex(@"'(?<val>.*?)'")]
    public static partial Regex GetArrayValueRegex { get; }

    [GeneratedRegex(@"project\s*\((?<content>.*?)\)", RegexOptions.Singleline)]
    public static partial Regex GetProjectBlock { get; }

    [GeneratedRegex(@"^\s*'(?<name>.*?)'\s*,\s*'(?<lang>.*?)'")]
    public static partial Regex GetProjectNameAndLanguage { get; }

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

    [GeneratedRegex(
        pattern:
        @"(?:if\s+(?<condition>build_machine\.system\(\)\s*==\s*'(?<system>[^']+)'"+
        @"(?:\s+or\s+build_machine\.system\(\)\s*==\s*'[^']+')*)\s+)?" +
        @"(?<var>\w+)\s*=\s*dependency\(\s*'(?<lib>[^']+)'" +  
        @"(?<args>(?:[^)]|'[^']*'|\((?:[^)]|'[^']*')*\))*)\)",   
        options: RegexOptions.Singleline | RegexOptions.IgnoreCase,
        cultureName: "en-US"
    )]
    public static partial Regex MesonDependencyRegex { get; }
   
    // Secondary regex to extract fallbacks from the 'args' group above
    [GeneratedRegex(@"fallback\s*:\s*\[(?<fallbacks>[^\]]+)\]")]
    public static partial Regex MesonFallbackRegex { get; }


}