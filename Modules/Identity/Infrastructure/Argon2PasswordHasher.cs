using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
using Microsoft.Extensions.Options;

namespace ShiftManagement.Api.Modules.Identity.Infrastructure;

public sealed class Argon2PasswordHasher(IOptions<Argon2Options> options)
{
    private readonly Argon2Options _options = options.Value;

    public string Hash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(_options.SaltLength);

        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = _options.Parallelism,
            Iterations = _options.Iterations,
            MemorySize = _options.MemoryKb
        };

        var hash = argon2.GetBytes(_options.HashLength);

        return $"argon2id${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
    }

    public bool Verify(string password, string storedHash)
    {
        var parts = storedHash.Split('$');

        if (parts.Length != 3 || parts[0] != "argon2id")
            return false;

        var salt = Convert.FromBase64String(parts[1]);
        var expectedHash = Convert.FromBase64String(parts[2]);

        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = _options.Parallelism,
            Iterations = _options.Iterations,
            MemorySize = _options.MemoryKb
        };

        var computedHash = argon2.GetBytes(_options.HashLength);

        return CryptographicOperations.FixedTimeEquals(computedHash, expectedHash);
    }
}