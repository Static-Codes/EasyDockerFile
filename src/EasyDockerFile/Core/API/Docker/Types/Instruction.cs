namespace EasyDockerFile.Core.API.Docker.Types;

public class Instruction {
    public string? Name;
    public string? Description;
    public List<InstructionOption> InstructionOptions = [];

    public bool SupportsEnvironmentVariables = false;
}