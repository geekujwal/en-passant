using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Stella.Core.DocumentStore
{
    public interface IDocument
    {
        [BsonId]
        string Id { get; }

        [BsonRepresentation(BsonType.String)]
        DateTimeOffset Created { get; set; }

        [BsonRepresentation(BsonType.String)]
        DateTimeOffset Modified { get; set; }
    }
}
