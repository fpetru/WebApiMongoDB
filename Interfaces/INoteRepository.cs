using System.Collections.Generic;
using System.Threading.Tasks;
using NotebookAppApi.Model;

namespace NotebookAppApi.Interfaces
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetAllNotes();
        Task<Note> GetNote(string id);
        void AddNote(Note item);
        Task<bool> RemoveNote(string id);
        void UpdateNote(string id, string body);

        // should be used with cautious, only in relation with demo setup
        void RemoveAllNotes();
    }
}
