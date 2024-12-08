using Stella.Core.DocumentStore;
using EnPassant.Lilith.Documents;
using Microsoft.AspNetCore.Http.HttpResults;
using Stella.Core.ErrorHandling;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using EnPassant.Lilith.Contracts;

namespace EnPassant.Lilith.Services;

//TODO: Make different package for abstraction 
public interface IUserService
{
    public Task RegisterUser(RequestRegistrationTokenRequest request, CancellationToken cancellationToken);
    public Task Login(CancellationToken cancellationToken);
}

public class UserService : IUserService
{
    public readonly IDocumentStore<UserDocument> _userDocumentStore;

    public UserService(IDocumentStore<UserDocument> userDocument)
    {
        _userDocumentStore = userDocument;
    }

    public Task Login(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task RegisterUser(RequestRegistrationTokenRequest request, CancellationToken cancellationToken)
    {
        var filter = Filters.User.ByUsername(request.Username);
        var user = await _userDocumentStore.GetDocumentAsync(filter, cancellationToken);
        if (user is not null)
        {
            throw new BadRequestException("User already exist");
        }
        user = new UserDocument()
        {
            Id = Guid.NewGuid().ToString(),
            Username = request.Username,
            Hash = HashPassword(request.Passowrd)
        };
        await _userDocumentStore.InsertDocumentAsync(user, cancellationToken);
    }
    private static string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);

        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password!,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
        return $"{Convert.ToBase64String(salt)}:{hashed}";
    }

    private static bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        var parts = hashedPassword.Split(':');
        if (parts.Length != 2)
        {
            return false;
        }

        byte[] salt = Convert.FromBase64String(parts[0]);
        var storedHash = parts[1];

        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: providedPassword,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

        return storedHash == hashed;
    }
}
