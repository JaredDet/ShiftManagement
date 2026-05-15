namespace ShiftManagement.Api.Modules.Identity.Infrastructure;

public sealed class Argon2Options
{
    public int MemoryKb { get; set; }
    public int Iterations { get; set; }
    public int Parallelism { get; set; }
    public int HashLength { get; set; }
    public int SaltLength { get; set; }
}