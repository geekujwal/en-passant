using MongoDB.Driver;
using EnPassant.Lilith.Documents;

namespace EnPassant.Lilith.Services;

public static class Filters
{
    public static class User
    {
        public static FilterDefinition<UserDocument> ById(string id)
        {
            return Builders<UserDocument>.Filter.Eq(doc => doc.Id, id);
        }

        public static FilterDefinition<UserDocument> ByUsername(string username)
        {
            return Builders<UserDocument>.Filter.Eq(doc => doc.Username, username);
        }
    }
}