using EasyDockerFile.Core.Types.BuildTypes.Base;

namespace EasyDockerFile.Core.Extensions;

public static class ToolchainExtension 
{
    public static string GetName(this ToolchainName name) => nameof(name);
}