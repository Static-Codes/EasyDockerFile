using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Spectre.Console.Cli;

namespace EasyDockerFile.Core.Common.Commands;

// Resolving all members that touch MainMenuSettings, even if they are explicitly called.
// Resolves warning IL3050: 
// Using member 'Spectre.Console.Cli.CommandApp<TDefaultCommand>.CommandApp(ITypeRegistrar)' 
// which has 'RequiresDynamicCodeAttribute' can break functionality when AOT compiling. 
// Spectre.Console.Cli relies on reflection.
// Use during trimming and AOT compilation is not supported and may result in unexpected behaviors.
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public class MainMenuSettings : CommandSettings
{
    // -p
    // --private
    [CommandOption("-p|--private")]
    [Description("Indicates the repository is private.")]
    [DefaultValue(false)]
    public bool PrivateFlagSet { get; init; }

    // -r=https://github.com/user/repo
    // --repo=https://github.com/user/repo
    // --repository=https://github.com/user/repo
    [CommandOption("-r|--repo|--repository <VALUE>")]
    [Description("Specifies the repository you are attempting to build from.")]
    [DefaultValue(null)]
    public required string? RepoLink { get; init; }

    // -t=<TOKEN>
    // --token=<TOKEN>
    [CommandOption("-t|--token <VALUE>")]
    [Description("Specifies the OAuth token.")]
    [DefaultValue(null)]
    public string? OAuthToken { get; init; }
}