using System.Runtime.InteropServices;
using static EasyDockerFile.Core.Common.Constants;

namespace EasyDockerFile.Core.Extensions;

public static class StringExtension
{
    public static string AsPrettyPrintedBranchString(this IEnumerable<string> branchNames) {
        if (!branchNames.Any()) {
            return $"- Unable to resolve, please check your OAuth Token above.";
        }

        return $"- {string.Join($"{NLC}    - ", branchNames)}";
    }

    public static string AsPrettyPrintedPathList(this IEnumerable<string> filePaths) {
        if (!filePaths.Any()) {
            return $"- Unable to resolve, please check your selected branch above.";
        }

        return $"- {string.Join($"{NLC}    - ", filePaths)}";
    }
    
    public static bool IsUnix(this string SystemName) {
        return MesonUnixSystemNames.Contains(SystemName);
    }


}