namespace ShiftManagement.Api.BuildingBlocks.Storage;

public interface IFileStorage
{
    Task<string> SaveAsync(
        StorageCategory category,
        string fileName,
        string base64Content,
        string mimeType);
}