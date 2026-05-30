using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

var repositoryRoot = FindRepositoryRoot(AppContext.BaseDirectory);
var version = GetVersion(repositoryRoot);
var generatedAt = GetCommitTimestamp(repositoryRoot);
var repositoryUrl = GetRepositoryUrl(repositoryRoot);
var sourceFiles = DiscoverSourceFiles(repositoryRoot);

var distDirectory = Path.Combine(repositoryRoot, "dist");
Directory.CreateDirectory(distDirectory);

var bundleFileName = $"igam-full-spec-{version}.md";
var bundlePath = Path.Combine(distDirectory, bundleFileName);
var latestPath = Path.Combine(distDirectory, "latest.md");
var manifestPath = Path.Combine(distDirectory, "manifest.json");

var bundleContent = BuildBundle(version, generatedAt, repositoryUrl, sourceFiles, repositoryRoot);
File.WriteAllText(bundlePath, bundleContent, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
File.WriteAllText(latestPath, bundleContent, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));

var manifest = new Manifest(
    Framework: "IGAM",
    Version: version,
    GeneratedAt: generatedAt.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"),
    BundleFile: bundleFileName,
    SourceFiles: sourceFiles.Select(file => ToRepositoryPath(repositoryRoot, file)).ToArray());

var manifestJson = JsonSerializer.Serialize(manifest, new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = true
});

File.WriteAllText(manifestPath, manifestJson + Environment.NewLine, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));

Console.WriteLine($"Generated {ToRepositoryPath(repositoryRoot, bundlePath)}");
Console.WriteLine($"Generated {ToRepositoryPath(repositoryRoot, latestPath)}");
Console.WriteLine($"Generated {ToRepositoryPath(repositoryRoot, manifestPath)}");

static string FindRepositoryRoot(string startDirectory)
{
    var directory = new DirectoryInfo(startDirectory);

    while (directory is not null)
    {
        if (Directory.Exists(Path.Combine(directory.FullName, ".git")))
        {
            return directory.FullName;
        }

        directory = directory.Parent;
    }

    throw new InvalidOperationException("Could not find repository root.");
}

static string GetVersion(string repositoryRoot)
{
    var candidates = new[]
    {
        Environment.GetEnvironmentVariable("GITHUB_REF_NAME"),
        RunGit(repositoryRoot, "describe", "--tags", "--exact-match")
    };

    var version = candidates.FirstOrDefault(candidate => !string.IsNullOrWhiteSpace(candidate))?.Trim();

    if (version is null)
    {
        throw new InvalidOperationException("No release version found. Run this tool from a vX.Y.Z tag, or set GITHUB_REF_NAME in CI.");
    }

    if (!Regex.IsMatch(version, @"^v\d+\.\d+\.\d+$"))
    {
        throw new InvalidOperationException($"Release version '{version}' must match vX.Y.Z.");
    }

    return version;
}

static DateTimeOffset GetCommitTimestamp(string repositoryRoot)
{
    var timestamp = RunGit(repositoryRoot, "log", "-1", "--format=%cI");

    if (DateTimeOffset.TryParse(timestamp, out var parsed))
    {
        return parsed.ToUniversalTime();
    }

    throw new InvalidOperationException("Could not read current commit timestamp.");
}

static string GetRepositoryUrl(string repositoryRoot)
{
    var url = RunGit(repositoryRoot, "remote", "get-url", "origin");
    return string.IsNullOrWhiteSpace(url) ? "unknown" : NormalizeRepositoryUrl(url.Trim());
}

static string NormalizeRepositoryUrl(string url)
{
    if (url.StartsWith("git@github.com:", StringComparison.OrdinalIgnoreCase))
    {
        url = "https://github.com/" + url["git@github.com:".Length..];
    }

    return url.EndsWith(".git", StringComparison.OrdinalIgnoreCase) ? url[..^4] : url;
}

static string[] DiscoverSourceFiles(string repositoryRoot)
{
    var roots = new[] { "docs", "examples", "schemas" };

    return roots
        .Select(root => Path.Combine(repositoryRoot, root))
        .Where(Directory.Exists)
        .SelectMany(root => Directory.EnumerateFiles(root, "*.md", SearchOption.AllDirectories))
        .OrderBy(file => ToRepositoryPath(repositoryRoot, file), StringComparer.Ordinal)
        .ToArray();
}

static string BuildBundle(
    string version,
    DateTimeOffset generatedAt,
    string repositoryUrl,
    IReadOnlyList<string> sourceFiles,
    string repositoryRoot)
{
    var builder = new StringBuilder();

    builder.AppendLine("# IGAM Full Specification");
    builder.AppendLine();
    builder.AppendLine($"Version: {version}");
    builder.AppendLine();
    builder.AppendLine($"Generated: {generatedAt:yyyy-MM-dd'T'HH:mm:ss'Z'}");
    builder.AppendLine();
    builder.AppendLine("This file is generated automatically.");
    builder.AppendLine();
    builder.AppendLine("Do not edit manually.");
    builder.AppendLine();
    builder.AppendLine("Source repository:");
    builder.AppendLine(repositoryUrl);
    builder.AppendLine();
    builder.AppendLine("## Table Of Contents");
    builder.AppendLine();

    foreach (var sourceFile in sourceFiles)
    {
        var repositoryPath = ToRepositoryPath(repositoryRoot, sourceFile);
        builder.AppendLine($"- [{repositoryPath}](#{ToAnchor(repositoryPath)})");
    }

    foreach (var sourceFile in sourceFiles)
    {
        var repositoryPath = ToRepositoryPath(repositoryRoot, sourceFile);
        var content = File.ReadAllText(sourceFile);

        builder.AppendLine();
        builder.AppendLine("---");
        builder.AppendLine();
        builder.AppendLine($"<a id=\"{ToAnchor(repositoryPath)}\"></a>");
        builder.AppendLine();
        builder.AppendLine($"SOURCE: {repositoryPath}");
        builder.AppendLine();
        builder.AppendLine("---");
        builder.AppendLine();
        builder.AppendLine(content.TrimEnd());
        builder.AppendLine();
    }

    return builder.ToString();
}

static string ToAnchor(string repositoryPath)
{
    var normalized = repositoryPath.ToLowerInvariant();
    var builder = new StringBuilder("source-");

    foreach (var character in normalized)
    {
        builder.Append(char.IsAsciiLetterOrDigit(character) ? character : '-');
    }

    return Regex.Replace(builder.ToString(), "-+", "-").Trim('-');
}

static string ToRepositoryPath(string repositoryRoot, string path)
{
    return Path.GetRelativePath(repositoryRoot, path).Replace(Path.DirectorySeparatorChar, '/');
}

static string RunGit(string repositoryRoot, params string[] arguments)
{
    var processStartInfo = new ProcessStartInfo("git")
    {
        WorkingDirectory = repositoryRoot,
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        UseShellExecute = false
    };

    foreach (var argument in arguments)
    {
        processStartInfo.ArgumentList.Add(argument);
    }

    using var process = Process.Start(processStartInfo);

    if (process is null)
    {
        return string.Empty;
    }

    var output = process.StandardOutput.ReadToEnd().Trim();
    process.WaitForExit();

    return process.ExitCode == 0 ? output : string.Empty;
}

internal sealed record Manifest(
    string Framework,
    string Version,
    string GeneratedAt,
    string BundleFile,
    string[] SourceFiles);
