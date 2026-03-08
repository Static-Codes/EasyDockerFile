using EasyDockerFile.Core.API.PackageSearch.Base;
using EasyDockerFile.Core.API.PackageSearch.Manifests;
using EasyDockerFile.Core.Helpers;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Serialization;
using static EasyDockerFile.Core.Common.RequestManager.NetworkClient;
using static System.Runtime.InteropServices.Architecture;

namespace EasyDockerFile.Core.API.PackageSearch;

// <summary>
// Implementation of PackageSearchApi for Fedora-based distros.
// <param name="architecture"> The desired architecture for the returned packages </param>
// </summary>
public class FedoraPackageApi : PackageSearchApi 
{
    // public string? PlatformIdentifer;
    private readonly Task<string?> _getUriTask;
    public new string? PlatformIdentifer;
    
    public FedoraPackageApi(Architecture architecture, string fedoraVersion, object? nullObj = null)
    {
        Console.WriteLine("[INFO]: Initializing _getUriTask");
        _getUriTask = InitializeGetUriTask(architecture, fedoraVersion);
        PlatformIdentifer = GetPlatformIdentifer(architecture);
        Console.WriteLine("[SUCCESS]: Initialized _getUriTask");
    }
    
    // If for some reason the Task above did not complete as intended, the quick and dirty fix is .GetAwaiter().GetResult()
    public override string? PackageFileUri => _getUriTask.Status == TaskStatus.RanToCompletion 
        ? _getUriTask.Result 
        : _getUriTask.GetAwaiter().GetResult();



    public override string[] FallbackPackages => ["git", "curl", "wget", "nano", "vim"];
    public List<DebianManifest> PackageManifests = [];
    

    private async Task<FileStream?> DownloadManifestFile() 
    {
        Console.WriteLine("[INFO]: Downloading compressed Fedora package manifest (~34MB)");
        var compressedStream = await Instance.GetStreamAsync(PackageFileUri!);
        Console.WriteLine("[SUCCESS]: Downloaded successful");
        
        // Add check if 250MB of storage is free.

        Console.WriteLine("[INFO]: Decompressing the compressed package manifest (~190MB)");
        using var decompressor = new GZipStream(compressedStream, CompressionMode.Decompress);
        var tempFile = Path.GetTempFileName();
        FileStream? dataStream = null;
        try {
            dataStream = File.Create(tempFile);
            ArgumentNullException.ThrowIfNull(dataStream, nameof(dataStream));
        }
        catch (Exception ex) {
            Console.WriteLine("[WARNING]: Decompression failed");
            Console.WriteLine($"[ERROR]: {ex.Message}");
            Environment.Exit(1);
        }


        await decompressor.CopyToAsync(dataStream!, 100000);
        Console.WriteLine("[SUCCESS]: Decompression successful");

        Console.WriteLine("[SUCCESS]: Reset stream position");
        dataStream.Position = 0;
        return dataStream;
    }

    /// <summary>
    /// Returns the Platform identifier associated with the specified architecture <br/>
    /// Source: https://fedoraproject.org/wiki/Architectures
    /// </summary>
    private static string? GetPlatformIdentifer(Architecture arch) => arch switch {
        X64 => "x86_64", 
        Arm64 => "aarch64", 
        S390x => "s390x",
        Ppc64le => "ppc64le",
        _ => null,
    };

    /// <summary>
    /// Returns the path to the dnf metadata xml file.
    /// </summary>
    private static string GetMetadataXMLFile(Architecture arch, string fedoraVersion) {
        
        var PlatformID = GetPlatformIdentifer(arch);

        ArgumentNullException.ThrowIfNull(PlatformID);

        return $"https://dl.fedoraproject.org/pub/fedora/linux/releases/{fedoraVersion}/Everything/{PlatformID}/os/repodata/";
    }
    

    // Ensures all types that touch RepoMD are preserved, even if they arent explicitly used.
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(RepoMD))]
    // [UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "Roots.xml handles the preservation of these types.")]
    // [UnconditionalSuppressMessage("AotAnalysis", "IL3050", Justification = "Due to the analysis above, currently, this method is AOT safe.")]
    private async Task<string?> InitializeGetUriTask(Architecture architecture, string fedoraVersion) 
    {   
        var repoDataRemoteDir = GetMetadataXMLFile(architecture, fedoraVersion);
        Stream? repoXMLFileStream = null;

        try 
        {
            var manifestXMLFile = Path.Combine(repoDataRemoteDir, "repomd.xml");
            repoXMLFileStream = await Instance.GetStreamAsync(manifestXMLFile!);
            XmlSerializer serializer = new XmlSerializer(typeof(RepoMD));
            
            ArgumentNullException.ThrowIfNull(repoXMLFileStream, nameof(repoXMLFileStream));
            var repoXMLFileObj = (RepoMD)serializer.Deserialize(repoXMLFileStream)!;
            ArgumentNullException.ThrowIfNull(repoXMLFileObj, nameof(repoXMLFileObj));


            var dataBlocks = repoXMLFileObj.Data;
            ArgumentNullException.ThrowIfNull(dataBlocks, nameof(dataBlocks));

            var desiredBlock = dataBlocks
                                .Where(block => block.Type == "primary_zck")
                                .FirstOrDefault();

            
            ArgumentNullException.ThrowIfNull(desiredBlock, nameof(desiredBlock));

            
            var baseUri = string.Format(
                "https://dl.fedoraproject.org/pub/fedora/linux/releases/{0}/Everything/{1}/os/",
                fedoraVersion,
                PlatformIdentifer
            );

            ArgumentNullException.ThrowIfNull(desiredBlock.Location);
            ArgumentNullException.ThrowIfNull(desiredBlock.Location.Href);

            return Path.Combine(baseUri, desiredBlock.Location.Href);
        }

        catch (Exception ex) {
            Console.WriteLine("[WARNING]: Unable to initialize _getUriTask");
            Console.WriteLine($"[ERROR TYPE]: {ex.GetType().Name}");
            Console.WriteLine($"[ERROR]: {ex.Message}");
        }

        return null;

    }

    /// <summary>
    /// Initializes FedoraPackageApi.PackageManifests <br/>
    /// This must be called before accessing FedoraPackageApi.PackageManifests
    /// </summary>
    public async Task InitializeManifestList() 
    {
        FileStream? manifestStream = await DownloadManifestFile();
    }

    private async Task ParseZCKArchiveInChunksAsync(Stream compressedStream)
    {
        var tempZck = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".xml.zck");
        var tempXml = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".xml");
        var unzckBinary = ZChunkLoader.Load();

        ArgumentNullException.ThrowIfNull(unzckBinary);

        try
        {
            using (var fs = new FileStream(tempZck, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
            {
                await compressedStream.CopyToAsync(fs);
            }
            
            
            var startInfo = new ProcessStartInfo
            {
                FileName = unzckBinary,
                Arguments = $"-o \"{tempXml}\" \"{tempZck}\"",
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true
            };

            using var process = Process.Start(startInfo);
            await process!.WaitForExitAsync();

            if (process.ExitCode != 0) {
                throw new Exception($"unzck failed: {await process.StandardError.ReadToEndAsync()}");
            }
            
            
            using (var xmlFileStream = new FileStream(tempXml, FileMode.Open, FileAccess.Read))
            using (var reader = XmlReader.Create(xmlFileStream, new XmlReaderSettings { Async = true }))
            {
                while (await reader.ReadAsync())
                {
                    // instead of reading the entire ~190MB raw XML stream, XmlReader reads in small kilobyte chunks.
                }
            }
        }

        finally
        {
            if (File.Exists(tempZck)) { 
                File.Delete(tempZck); 
            }

            if (File.Exists(tempXml)) { 
                File.Delete(tempXml); 
            }
        }
    }
    
}