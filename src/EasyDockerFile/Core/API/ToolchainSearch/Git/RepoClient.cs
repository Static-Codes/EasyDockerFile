namespace EasyDockerFile.Core.API.ToolchainSearch.Git;

using EasyDockerFile.Core.Extensions;
using EasyDockerFile.Core.Types.GitTypes;
using Octokit;
using System;
using System.Linq;
using System.Threading.Tasks;

public class RepoClient
{
    private readonly GitHubClient _client;
    private readonly RepoInfo _repoInfo;
    private readonly ApiInfo apiInfo;

    public RepoClient(RepoInfo repoInfoObj)
    {
        _repoInfo = repoInfoObj;

        _client = new GitHubClient(new ProductHeaderValue(_repoInfo.RepoUrlObj.RepoName));

        if (_repoInfo.RequiresAuth) {
            _client.Credentials = _repoInfo.Authentication;

            // This needs improvement, async calls should not called on a blocking thread
            _repoInfo.UserInfo = _client.User.Current().GetAwaiter().GetResult();
        }
        apiInfo = _client.GetLastApiInfo();
    }

    private bool BranchesFound() {
        return _repoInfo.BranchNames.Any();
    }

    public (bool IsRateLimited, Tuple<int, int, DateTimeOffset>? RateLimitInfo) GetRateLimitInfo()
    {
        var rateLimit = apiInfo?.RateLimit;

        if (rateLimit == null)
        {
            return (false, null);
        }

        var rateLimitInfo = Tuple.Create(
            rateLimit.Limit, 
            rateLimit.Remaining, 
            rateLimit.Reset
        );

        return (true, rateLimitInfo);
    }

    public async Task UpdateBranchesAsync()
    {
        if (_client.Repository == null) {
            Console.WriteLine("[WARNING]: Unable to locate a repository at the provided uri.");
            Console.WriteLine("[ERROR]: _client.Repository is null in UpdateBranchesAsync()");
            Console.WriteLine("[INFO]: If you are positive the uri you provided is correct, please run:");
            Console.WriteLine("[COMMAND]: easy-dockerfile --private url/to/repo");
            Environment.Exit(1);
        }
        IReadOnlyList<Branch> branchesObj = [];
        try {
            branchesObj = await _client.Repository.Branch.GetAll(
                _repoInfo.RepoUrlObj.Username,
                _repoInfo.RepoUrlObj.RepoName 
            );
        }

        catch (NotFoundException) {
            Console.WriteLine("[WARNING]: Unable to locate branches at the provided repository uri.");
            Console.WriteLine($"[ERROR TYPE]: NotFoundException");
            Console.WriteLine($"[ERROR]: Repository uri `{_repoInfo.RepoUrlObj.GetAbsoluteUrl()}` was not resolved.");
            Console.WriteLine("[INFO]: If you are positive the uri you provided is correct, please run:");
            Console.WriteLine("[COMMAND]: easy-dockerfile --private url/to/repo");
            Environment.Exit(1);
        }

        _repoInfo.BranchNames = branchesObj.Select(branch => branch.Name) ?? [];
    }
    
    public void UpdateStatus() {
        _repoInfo.Status = BranchesFound() ? RepoStatus.Public : _repoInfo.Status; 
    }



    public override string ToString()
    {
        return $"""
        -----------------------------------
        - RepoUrl: 
            - Protocol: {_repoInfo!.RepoUrlObj.Protocol}
            - Domain: {_repoInfo.RepoUrlObj.Domain}
            - Username: {_repoInfo.RepoUrlObj.Username}
            - RepoName: {_repoInfo.RepoUrlObj.RepoName}
        -----------------------------------
        - Status: {_repoInfo.Status}
        -----------------------------------
        - Authentication: 
            - Is Set: {_repoInfo.Authentication != null}
            - OAuth Token: {_repoInfo.Authentication.GetToken()}
        -----------------------------------
        - User Info:
            - Is Authenticated: {_repoInfo.UserInfo != null}
            - Username: {_repoInfo.UserInfo?.Login ?? "Not Set"}
            - Public Repos: {_repoInfo.UserInfo?.PublicRepos.ToString() ?? "Not Set"}
        -----------------------------------
        - Branches:
            {_repoInfo.BranchNames.AsPrettyPrintedBranchString()}
        -----------------------------------
        - Is Private: {_repoInfo.IsPrivate}
        -----------------------------------
        - Is Valid: {_repoInfo.IsValid}
        -----------------------------------
        - RequiresAuth: {_repoInfo.RequiresAuth}
        """;
    }
}