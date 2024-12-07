using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Stella.Core.DocumentStore;

namespace EnPassant.Lilith.Documents
{
    public class UserDocument : IDocument
    {
        public string Id { get; set; }

        private string _email;

        public string FullName { get; set; }

        public string Email
        {
            get => _email;
            set => _email = value?.ToLowerInvariant().Trim();
        }

        public string Hash { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTimeOffset Created { get; set; } = DateTime.Now;

        [BsonRepresentation(BsonType.DateTime)]
        public DateTimeOffset Modified { get; set; } = DateTime.Now;
    }
}
