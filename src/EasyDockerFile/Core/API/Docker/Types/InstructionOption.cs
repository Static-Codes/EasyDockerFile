namespace EasyDockerFile.Core.API.Docker.Types;

public class InstructionOption {
    public string OptionName { get; set; } = "";
    public string Description { get; set; } = "";

    // This will be overriden if needbe;
    public string MinimumDockerVersionSupported { get; set; } = "1.0";
}