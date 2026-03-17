using DockerFileSharp.Common.Image;

namespace DockerFileSharp.Common;

public class DockerImage(IsoImage selectedImage) 
{
    public string ImageName => GetImageName(selectedImage);

    private static string GetImageName(IsoImage Image)
    {
        // Currently this is excessive as full name could be parsed.
        // However, with future maintenance in mind, this is a more robust solution. 
        string baseName = Image.FullName.Split(' ')[0].ToLower();
        string version = Image.Version.ToLower();

        return $"{baseName}:{version}";
    }
}