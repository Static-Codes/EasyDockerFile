namespace DockerFileSharp.Common;

// See Docs/Methodology.md for more information about implementating IDockerInstruction.
public interface IDockerInstruction
{
    public static readonly string? Description;
    public static readonly bool SupportsEnvironmentVariables;
    public string Build();
}
