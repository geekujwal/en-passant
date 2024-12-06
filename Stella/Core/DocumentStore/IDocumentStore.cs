using MongoDB.Bson;
using MongoDB.Driver;

namespace Stella.Core.DocumentStore
{
    public interface IDocumentStore<TDocument> where TDocument : class, IDocument
    {
        Task<TDocument> GetDocumentAsync(string documentId, CancellationToken cancellationToken);

        Task RemoveDocument(string documentId, CancellationToken cancellationToken);

        Task<TDocument> GetDocumentAsync(FilterDefinition<TDocument> filter, CancellationToken cancellationToken);

        Task InsertDocumentAsync(TDocument document, CancellationToken cancellationToken);

        Task<IEnumerable<TDocument>> GetDocumentsAsync(FilterDefinition<TDocument> filter, CancellationToken cancellationToken);

        Task<long> GetDocumentsCountAsync(FilterDefinition<TDocument> filter, CancellationToken cancellationToken);

        Task<(int PageCount, IReadOnlyList<BsonDocument> Documents)> GetPagedDocumentsAsync(FilterDefinition<TDocument> filter,
        SortDefinition<TDocument> sort,
        int page,
        int pageSize,
        CancellationToken cancellationToken);
        Task SaveAsync(TDocument document, CancellationToken cancellationToken, bool throwOnVersionConflict = true);

    }
}
