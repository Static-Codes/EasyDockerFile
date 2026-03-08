using System.Xml.Serialization;

// using System.Xml.Serialization;
// XmlSerializer serializer = new XmlSerializer(typeof(Metadata));
// using (StringReader reader = new StringReader(xml))
// {
//    var test = (Metadata)serializer.Deserialize(reader);
// }

namespace EasyDockerFile.Core.API.PackageSearch.Manifests;



// These are used to serialize repomd.xml
// Located at:
// https://dl.fedoraproject.org/pub/fedora/linux/releases/{fedoraVersion}/Everything/{architecture}/os/repodata/repomd.xml

[XmlRoot(ElementName="checksum")]
public class RepoMDChecksum { 

	[XmlAttribute(AttributeName="type")] 
	public string? Type; 

	[XmlText] 
	public string? Text; 
}


[XmlRoot(ElementName="open-checksum")]
public class RepoMDOpenChecksum { 

	[XmlAttribute(AttributeName="type")] 
	public string? Type; 

	[XmlText] 
	public string? Text; 
}

[XmlRoot(ElementName="location")]
public class RepoMDLocation { 

	[XmlAttribute(AttributeName="href")] 
	public string? Href; 
}

[XmlRoot(ElementName="data")]
public class RepoMDData { 

	[XmlElement(ElementName="checksum")] 
	public Checksum? Checksum; 

	[XmlElement(ElementName="open-checksum")] 
	public RepoMDOpenChecksum? OpenChecksum; 

	[XmlElement(ElementName="location")] 
	public Location? Location; 

	[XmlElement(ElementName="timestamp")] 
	public int Timestamp; 

	[XmlElement(ElementName="size")] 
	public int Size; 

	[XmlElement(ElementName="open-size")] 
	public int Opensize; 

	[XmlAttribute(AttributeName="type")] 
	public string? Type; 

	[XmlText] 
	public string? Text; 
}

[XmlRoot(ElementName="repomd")]
public class RepoMD { 

	[XmlElement(ElementName="revision")] 
	public int Revision; 

	[XmlElement(ElementName="data")] 
	public RepoMDData[]? Data; 

	[XmlAttribute(AttributeName="xmlns")] 
	public string? Xmlns; 

	[XmlAttribute(AttributeName="rpm")] 
	public string? Rpm; 

	[XmlText] 
	public string? Text; 
}







[XmlRoot(ElementName="version")]
public class Version { 

	[XmlAttribute(AttributeName="epoch")] 
	public int Epoch; 

	[XmlAttribute(AttributeName="ver")] 
	public string? Ver; 

	[XmlAttribute(AttributeName="rel")] 
	public string? Rel; 
}


[XmlRoot(ElementName="checksum")]
public class Checksum { 

	[XmlAttribute(AttributeName="type")] 
	public string? Type; 

	[XmlAttribute(AttributeName="pkgid")] 
	public string? Pkgid; 

	[XmlText] 
	public string? Text; 
}


[XmlRoot(ElementName="time")]
public class Time { 

	[XmlAttribute(AttributeName="file")] 
	public int File; 

	[XmlAttribute(AttributeName="build")] 
	public int Build; 
}


[XmlRoot(ElementName="size")]
public class Size { 

	[XmlAttribute(AttributeName="package")] 
	public int Package; 

	[XmlAttribute(AttributeName="installed")] 
	public int Installed; 

	[XmlAttribute(AttributeName="archive")] 
	public int Archive; 
}


[XmlRoot(ElementName="location")]
public class Location { 

	[XmlAttribute(AttributeName="href")] 
	public string? Href; 
}


[XmlRoot(ElementName="header-range")]
public class Headerrange { 

	[XmlAttribute(AttributeName="start")] 
	public int Start; 

	[XmlAttribute(AttributeName="end")] 
	public int End; 
}


[XmlRoot(ElementName="entry")]
public class Entry { 

	[XmlAttribute(AttributeName="name")] 
	public string? Name; 

	[XmlAttribute(AttributeName="flags")] 
	public string? Flags; 

	[XmlAttribute(AttributeName="epoch")] 
	public int Epoch; 

	[XmlAttribute(AttributeName="ver")] 
	public string? Ver; 

	[XmlAttribute(AttributeName="rel")] 
	public string? Rel; 
}


[XmlRoot(ElementName="provides")]
public class Provides { 

	[XmlElement(ElementName="entry")] 
	public List<Entry> Entry = []; 
}


[XmlRoot(ElementName="requires")]
public class Requires { 

	[XmlElement(ElementName="entry")] 
	public List<Entry> Entry = []; 
}


[XmlRoot(ElementName="format")]
public class Format { 

	[XmlElement(ElementName="license")] 
	public string? License; 

	[XmlElement(ElementName="vendor")] 
	public string? Vendor; 

	[XmlElement(ElementName="group")] 
	public string? Group; 

	[XmlElement(ElementName="buildhost")] 
	public string? Buildhost; 

	[XmlElement(ElementName="sourcerpm")] 
	public string? Sourcerpm; 

	[XmlElement(ElementName="header-range")] 
	public Headerrange? Headerrange; 

	[XmlElement(ElementName="provides")] 
	public Provides? Provides; 

	[XmlElement(ElementName="requires")] 
	public Requires? Requires; 

	[XmlElement(ElementName="file")] 
	public List<string>? File; 
}


[XmlRoot(ElementName="package")]
public class Package { 

	[XmlElement(ElementName="name")] 
	public string? Name; 

	[XmlElement(ElementName="arch")] 
	public string? Arch; 

	[XmlElement(ElementName="version")] 
	public Version? Version; 

	[XmlElement(ElementName="checksum")] 
	public Checksum? Checksum; 

	[XmlElement(ElementName="summary")] 
	public string? Summary; 

	[XmlElement(ElementName="description")] 
	public string? Description; 

	[XmlElement(ElementName="packager")] 
	public string? Packager; 

	[XmlElement(ElementName="url")] 
	public string? Url; 

	[XmlElement(ElementName="time")] 
	public Time? Time; 

	[XmlElement(ElementName="size")] 
	public Size? Size; 

	[XmlElement(ElementName="location")] 
	public Location? Location; 

	[XmlElement(ElementName="format")] 
	public Format? Format; 

	[XmlAttribute(AttributeName="type")] 
	public string? Type; 

	[XmlText] 
	public string? Text; 
}


[XmlRoot(ElementName="metadata")]
public class Metadata { 

	[XmlElement(ElementName="package")] 
	public Package? Package; 

	[XmlAttribute(AttributeName="xmlns")] 
	public string? Xmlns; 

	[XmlAttribute(AttributeName="rpm")] 
	public string? Rpm; 

	[XmlAttribute(AttributeName="packages")] 
	public int Packages; 

	[XmlText] 
	public string? Text; 
}


