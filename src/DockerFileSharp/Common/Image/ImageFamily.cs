using System.Xml.Serialization;

namespace DockerFileSharp.Common.Image;

[XmlRoot("family")]
public class ImageFamily
{
    // Parameterless constructor for XmlSerializer
    public ImageFamily() { }
    
    [XmlText]
    public string Name { get; set; } = string.Empty;

    [XmlElement("image")]
    public IsoImage[] Images { get; set; } = [];
}
