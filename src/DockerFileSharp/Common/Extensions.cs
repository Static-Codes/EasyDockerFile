using System.Runtime.InteropServices;

namespace DockerFileSharp.Common;

public static class Extensions 
{
    public static bool IsEmpty(this string? value) => string.IsNullOrWhiteSpace(value);

    public static Architecture ToArchitecture(this string archString) => archString switch {
        "x64" => Architecture.X64,
        "arm64" => Architecture.Arm64,
        "armhf" or "armel" => Architecture.Arm,
        _ => throw new InvalidOperationException($"Unable to parse '{archString}' to System.Runtime.InteropServices.Architecture")
    };
}
