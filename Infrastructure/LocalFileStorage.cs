using Microsoft.Extensions.Options;

namespace ShiftManagement.Api.Infrastructure;

public sealed class LocalFileStorage(IOptions<StorageOptions> options) : IFileStorage
{
    private readonly string _basePath = options.Value.Path;

    public async Task<string> SaveAsync(
        StorageCategory category,
        string fileName,
        string base64Content,
        string mimeType)
    {
        var bytes = Convert.FromBase64String(base64Content);

        var safeFileName = Path.GetFileName(fileName);
        var uniqueName = $"{Guid.NewGuid()}_{safeFileName}";

        var folder = Path.Combine(_basePath, GetFolder(category));
        Directory.CreateDirectory(folder);

        var fullPath = Path.Combine(folder, uniqueName);

        await File.WriteAllBytesAsync(fullPath, bytes);

        return $"/files/{GetFolder(category)}/{uniqueName}";
    }

    private static string GetFolder(StorageCategory category) => category switch
    {
        StorageCategory.ClaimsEvidences => "claims/evidences",
        StorageCategory.Staff => "staff",
        _ => throw new ArgumentOutOfRangeException(nameof(category), category, "Unknown storage category")
    };
}