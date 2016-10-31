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
        void RemoveAllNotes();
        void UpdateNote(string id, Note item);
    }
}
