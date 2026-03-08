using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.RuntimeInformation;

namespace EasyDockerFile.Core.Common;


public static class Platform
{
    public static bool IsWindows => IsOSPlatform(OSPlatform.Windows);
    public static bool IsMac => IsOSPlatform(OSPlatform.OSX);
    public static bool IsLinux => IsOSPlatform(OSPlatform.Linux);
    public static bool IsBSD => IsOSPlatform(OSPlatform.FreeBSD);
}