using DockerFileSharp.Instructions;

namespace DockerFileSharp.Sandbox;
public class DockerFileGenerationTest
{
    public static void AddInstructionTest() {
        AddInstruction instruction = new("TestSource", "TestDestination");
        Console.WriteLine(instruction.Build());
    }
    
}