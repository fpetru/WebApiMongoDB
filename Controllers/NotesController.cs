using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

using NotebookAppApi.Interfaces;
using NotebookAppApi.Model;
using NotebookAppApi.Data;

namespace NotebookAppApi.Controllers
{
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        private readonly INoteRepository _noteRepository;

        public NotesController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        // GET: notes/notes
        [HttpGet]
        public Task<string> Get()
        {
            return GetNoteInternal();
        }

        private async Task<string> GetNoteInternal()
        {
            var notes = await _noteRepository.GetAllNotes();
            return JsonConvert.SerializeObject(notes);
        }

        // GET api/notes/5
        [HttpGet("{id}")]
        public Task<string> Get(string id)
        {
            return GetNoteByIdInternal(id);
        }

        private async Task<string> GetNoteByIdInternal(string id)
        {
            var note = await _noteRepository.GetNote(id) ?? new Note();
            return JsonConvert.SerializeObject(note);
        }

        // POST api/notes
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/notes/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/notes/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
