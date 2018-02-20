using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotebookAppApi.Interfaces;
using NotebookAppApi.Model;
using NotebookAppApi.Infrastructure;
using System;
using System.Collections.Generic;

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

        [NoCache]
        [HttpGet]
        public async Task<IEnumerable<Note>> Get()
        {
            return await _noteRepository.GetAllNotes();
        }

        // GET api/notes/5
        [HttpGet("{id}")]
        public async Task<Note> Get(string id)
        {
            return await _noteRepository.GetNote(id) ?? new Note();
        }

        // POST api/notes
        [HttpPost]
        public void Post([FromBody] NoteParam newNote)
        {
            _noteRepository.AddNote(new Note
                                        {
                                            Id = newNote.Id,
                                            Body = newNote.Body,
                                            CreatedOn = DateTime.Now,
                                            UpdatedOn = DateTime.Now,
                                            UserId = newNote.UserId
                                        });
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
