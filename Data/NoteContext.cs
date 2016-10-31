using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NotebookAppApi.Model;

namespace NotebookAppApi.Data
{
    public class NoteContext
    {
        private readonly IMongoDatabase _database = null;

        public NoteContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Note> Notes
        {
            get
            {
                return _database.GetCollection<Note>("Note");
            }
        }
    }
}
