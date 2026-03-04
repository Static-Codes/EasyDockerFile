using static EasyDockerFile.Core.Helpers.ImageLoader;
using static EasyDockerFile.Core.Helpers.InputHelper;
using Spectre.Console;


var families = GetFamilies();
var familyNames = families.Select(fam => fam.Name);

var familyChoice = AskForInput("Please select your desired image family.", familyNames);
Console.WriteLine(familyChoice);



// var debianPackageApi = new DebianPackageApi(Architecture.X64);
// await debianPackageApi.InitializeManifestList();

// foreach (var manifest in debianPackageApi.PackageManifests) {
//     Console.WriteLine(manifest);
// }