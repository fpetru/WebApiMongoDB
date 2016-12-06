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
            return await _context.Notes
                            .Find(filter)
                            .FirstOrDefaultAsync();
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

        public async Task<UpdateResult> UpdateNote(string id, string body)
        {
            var filter = Builders<Note>.Filter.Eq(s => s.Id, id);
            var update = Builders<Note>.Update
                            .Set(s => s.Body, body)
                            .CurrentDate(s => s.UpdatedOn);
            return await _context.Notes.UpdateOneAsync(filter, update);
        }

        public async Task<ReplaceOneResult> UpdateNote(string id, Note item)
        {
            return await _context.Notes
                            .ReplaceOneAsync(n => n.Id.Equals(id)
                                            , item
                                            , new UpdateOptions { IsUpsert = true });
        }

        // Demo function - full document update
        public async Task<ReplaceOneResult> UpdateNoteDocument(string id, string body)
        {
            var item = await GetNote(id) ?? new Note();
            item.Body = body;
            item.UpdatedOn = DateTime.Now;

            return await UpdateNote(id, item);
        }

        public void RemoveAllNotes()
        {
            _context.Notes.DeleteManyAsync(new BsonDocument());
        }
    }
}
