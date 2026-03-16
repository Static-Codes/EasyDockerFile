using System.Text.RegularExpressions;

namespace EasyDockerFile.Core.Types.Build.Meson;

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
            // Project Name (String)
            @"project\s*\(\s*'(?<name>[^']+)'\s*,\s*" +

            // Language Name (String or Array)
            @"(?:'(?<lang_single>[^']+)'|\s*(?<lang_array>\[[^\]]*?\]))\s*" +

            // Project Version (String)
            @"(?:(?=[\s\S]*?\bversion\s*:\s*'(?<project_version>[^']+)')?)" +

            // Project License (String or Array)
            @"(?:(?=[\s\S]*?\blicense\s*:\s*(?:'(?<license_single>[^']+)'|\s*(?<license_array>\[[^\]]*?\])))?)" +

            // Meson Version (String)
            @"(?:(?=[\s\S]*?\bmeson_version\s*:\s*'(?<meson_version>[^']+)')?)" +

            // Default Options (String or Array)
            @"(?:(?=[\s\S]*?\bdefault_options\s*:\s*(?<build_arguments>\[[^\]]*?\]))?)" +

            // End of project block
            @"[\s\S]*?\)",
        options: RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace
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