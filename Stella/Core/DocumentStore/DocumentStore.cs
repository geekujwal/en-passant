using MongoDB.Bson;
using MongoDB.Driver;

namespace Stella.Core.DocumentStore
{
    public class DocumentStore<TDocument> : IDocumentStore<TDocument>
        where TDocument : class, IDocument
    {
        private readonly IMongoCollection<TDocument> _collection;
        public DocumentStore(
            MongoDbContext context
        )
        {
            _collection = context.GetCollection<TDocument>(GetCollectionName());
        }

        public async Task<TDocument> GetDocumentAsync(string documentId, CancellationToken cancellationToken)
        {
            var filter = Builders<TDocument>.Filter.Eq(f => f.Id, documentId);
            return await GetDocumentAsync(filter, cancellationToken);
        }

        public async Task<TDocument> GetDocumentAsync(FilterDefinition<TDocument> filter, CancellationToken cancellationToken)
        {
            return (await _collection.FindAsync(filter, cancellationToken: cancellationToken))?.ToList(cancellationToken: cancellationToken)?.FirstOrDefault();
        }

        public async Task InsertDocumentAsync(TDocument document, CancellationToken cancellationToken)
        {
            var insertOptions = new InsertOneOptions();

            await _collection.InsertOneAsync(document, insertOptions, cancellationToken);
        }

        public async Task<IEnumerable<TDocument>> GetDocumentsAsync(FilterDefinition<TDocument> filter, CancellationToken cancellationToken)
        {
            return (await _collection.FindAsync(filter, cancellationToken: cancellationToken)).ToEnumerable(cancellationToken: cancellationToken);
        }

        public async Task<long> GetDocumentsCountAsync(FilterDefinition<TDocument> filter, CancellationToken cancellationToken)
        {
            var countTask = _collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken);
            return await countTask.ConfigureAwait(false);
        }

        public async Task<(int PageCount, IReadOnlyList<BsonDocument> Documents)> GetPagedDocumentsAsync(
            FilterDefinition<TDocument> filter,
        SortDefinition<TDocument> sort,
        int page,
        int count,
        CancellationToken cancellationToken
        )
        {
            var totalCount = await _collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken);

            var totalPages = (int)Math.Ceiling((double)totalCount / count);

            var docs = await _collection
                .Find(filter)
                .Skip((page - 1) * count)
                .Limit(count)
                .Project(Builders<TDocument>.Projection.As<BsonDocument>())
                .ToListAsync(cancellationToken);

            return (totalPages, docs);
        }

        public async Task SaveAsync(TDocument document, CancellationToken cancellationToken, bool throwOnVersionConflict = true)
        {
            var idFilter = Builders<TDocument>.Filter.Eq(f => f.Id, document.Id);
            var options = new ReplaceOptions
            {
                IsUpsert = false,
                BypassDocumentValidation = false,
            };
            await _collection.ReplaceOneAsync(idFilter, document, options, cancellationToken);
            // todo look for version conflict too 
        }

        public async Task RemoveDocument(string documentId, CancellationToken cancellationToken)
        {
            var filter = Builders<TDocument>.Filter.Eq(f => f.Id, documentId);
            await _collection.DeleteOneAsync(filter, cancellationToken);
        }

        private static string GetCollectionName()
        {
            var name = typeof(TDocument).Name;
            return name;
        }
    }
}
