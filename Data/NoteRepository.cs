using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

using NotebookAppApi.Interfaces;
using NotebookAppApi.Model;
using MongoDB.Bson;

namespace NotebookAppApi.Data
{
    public class NoteRepository : INoteRepository
    {
        private readonly NoteContext _context = null;

        public NoteRepository(IOptions<Settings> settings)
        {
            _context = new NoteContext(settings);
        }

        public async Task<IEnumerable<Note>> GetAllNotes()
        {
            var documents = await _context.Notes.Find(_ => true)
                            .ToListAsync();
            return documents;
        }

        public async Task<Note> GetNote(string id)
        {
            var filter = Builders<Note>.Filter.Eq("Id", id);
            var document = await _context.Notes.Find(filter).FirstOrDefaultAsync();
            return document;
        }

        public async void AddNote(Note item)
        {
            await _context.Notes.InsertOneAsync(item);
        }

        public async Task<bool> RemoveNote(string id)
        {
            var result = await _context.Notes.DeleteOneAsync(
                 Builders<Note>.Filter.Eq("Id", id));

            return result.DeletedCount > 0;
        }

        public async void UpdateNote(string id, string body)
        {
            var filter = Builders<Note>.Filter.Eq(s => s.Id, id);
            var update = Builders<Note>.Update
                            .Set(s => s.Body, body)
                            .CurrentDate(s => s.UpdatedOn);
            var result = await _context.Notes.UpdateOneAsync(filter, update);
        }

        public void RemoveAllNotes()
        {
            _context.Notes.DeleteManyAsync(new BsonDocument());
        }
    }
}
