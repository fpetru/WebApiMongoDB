using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NotebookAppApi.Interfaces;
using NotebookAppApi.Model;
using NotebookAppApi.Infrastructure;
using System;
using Microsoft.AspNetCore.Cors;

namespace NotebookAppApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        private readonly INoteRepository _noteRepository;

        public NotesController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        // GET: notes/notes
        [NoCache]
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
            _noteRepository.AddNote(new Note() { Body = value, CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now });
        }

        // PUT api/notes/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]string value)
        {
            _noteRepository.UpdateNoteDocument(id, value);
        }

        // DELETE api/notes/23243423
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _noteRepository.RemoveNote(id);
        }
    }
}
