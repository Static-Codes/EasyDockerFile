using System.Xml.Serialization;

namespace EasyDockerFile.Core.Types.ImageTypes;

[XmlRoot("families")]
public class ImageFamilies 
{
    // Parameterless constructor for XmlSerializer
    public ImageFamilies() { }

    [XmlElement("family")]
    public ImageFamily[] SupportedFamilies { get; set; } = [];
}

