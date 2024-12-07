using Stella.Core.DocumentStore;
using EnPassant.Lilith.Documents;

namespace EnPassant.Lilith.Services;

public interface IUserService
{
    public Task TestApiAsync(CancellationToken cancellationToken);
}

public class UserService : IUserService
{
    public readonly IDocumentStore<UserDocument> _userDocumentStore;

    public UserService(IDocumentStore<UserDocument> userDocument)
    {
        _userDocumentStore = userDocument;

    }

    public async Task TestApiAsync(CancellationToken cancellationToken)
    {
        var doc = new UserDocument()
        {
            Id = "asdasd",
            Email = "itsujwal2019@gmail.com",
            FullName = "asdas",
            Hash = "asdasdsad"
        };
        await _userDocumentStore.InsertDocumentAsync(doc, cancellationToken);
    }
}
