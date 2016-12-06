using System.Collections.Generic;
using System.Threading.Tasks;
using NotebookAppApi.Model;
using MongoDB.Driver;

namespace NotebookAppApi.Interfaces
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetAllNotes();
        Task<Note> GetNote(string id);
        void AddNote(Note item);
        Task<bool> RemoveNote(string id);

        Task<UpdateResult> UpdateNote(string id, string body);

        // demo interface - full document update
        Task<ReplaceOneResult> UpdateNoteDocument(string id, string body);

        // should be used with cautious, only in relation with demo setup
        void RemoveAllNotes();
    }
}
