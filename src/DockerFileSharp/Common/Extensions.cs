namespace DockerFileSharp.Common;

public static class Entensions 
{
    public static bool IsEmpty(this string? value) => string.IsNullOrWhiteSpace(value);
}