namespace EasyDockerFile.Core.API.Docker.Types;

public static class Instructions 
{
    public readonly static List<Instruction> AllInstructions =
    [
        new Instruction { Name = "FROM", Description = "Create a new build stage from a base image.", SupportsEnvironmentVariables = true },
        new Instruction { Name = "ARG", Description = "Use build-time variables.", SupportsEnvironmentVariables = false },
        new Instruction { Name = "RUN", Description = "Execute build commands.", SupportsEnvironmentVariables = true, 
            InstructionOptions = [
                new InstructionOption { OptionName = "--mount", Description = "Allows you to create mounts for caching or secrets.", MinimumDockerVersionSupported = "18.09" },
                new InstructionOption { OptionName = "--network", Description = "Sets the network networking type.", MinimumDockerVersionSupported = "20.10" }
            ]
        },
        new Instruction { Name = "CMD", Description = "Specify default commands.", SupportsEnvironmentVariables = true },
        new Instruction { Name = "LABEL", Description = "Add metadata to an image.", SupportsEnvironmentVariables = true },
        new Instruction { Name = "EXPOSE", Description = "Describe which ports your application is listening on.", SupportsEnvironmentVariables = false },
        new Instruction { Name = "ENV", Description = "Set environment variables.", SupportsEnvironmentVariables = false },
        new Instruction { Name = "ADD", Description = "Add local or remote files and directories", SupportsEnvironmentVariables = true,
             InstructionOptions = [
                new InstructionOption { OptionName = "--chown", Description = "Sets user/group ownership.", MinimumDockerVersionSupported = "17.09" }
            ]
        },
        new Instruction { Name = "COPY", Description = "Copy files and directories.", SupportsEnvironmentVariables = true,
            InstructionOptions = [
                new InstructionOption { OptionName = "--from", Description = "Copies files from a previous build stage.", MinimumDockerVersionSupported = "17.05" }
            ]
        },
        new Instruction { Name = "ENTRYPOINT", Description = "Specify default executable.", SupportsEnvironmentVariables = true },
        new Instruction { Name = "VOLUME", Description = "Create volume mounts.", SupportsEnvironmentVariables = true },
        new Instruction { Name = "USER", Description = "Set user and group ID.", SupportsEnvironmentVariables = true },
        new Instruction { Name = "WORKDIR", Description = "Change working directory.", SupportsEnvironmentVariables = true },
        new Instruction { Name = "ONBUILD", Description = "Specify instructions for when the image is used in a build.", SupportsEnvironmentVariables = false },
        new Instruction { Name = "STOPSIGNAL", Description = "Specify the system call signal for exiting a container.", SupportsEnvironmentVariables = true },
        new Instruction { Name = "HEALTHCHECK", Description = "Check a container's health on startup.", SupportsEnvironmentVariables = true },
        new Instruction { Name = "SHELL", Description = "Set the default shell of an image.", SupportsEnvironmentVariables = false },
        new Instruction { Name = "MAINTAINER", Description = "Specify the author of an image (Deprecated).", SupportsEnvironmentVariables = false }
    ];
}