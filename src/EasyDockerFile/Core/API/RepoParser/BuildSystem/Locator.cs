using EasyDockerFile.Core.Types.Build.Base;

namespace EasyDockerFile.Core.API.RepoParser.BuildSystem;

public static class Locator 
{
    public static BuildSystemName? LocateBuildSystem(IEnumerable<string> files) 
    {
        if (files.Contains("meson.build")) {
            return BuildSystemName.Meson;
        }
        return null;
    }
}