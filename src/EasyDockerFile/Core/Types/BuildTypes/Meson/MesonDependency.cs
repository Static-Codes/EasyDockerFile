namespace EasyDockerFile.Core.Types.BuildTypes.Meson;

public class MesonDependency 
{
    public required string? SystemName { get; set; }
    public required string? VariableName { get; set; }
    public required string? LibraryName { get; set; }
    public required string[] FallbackLibraries { get; set; } = []; 
}