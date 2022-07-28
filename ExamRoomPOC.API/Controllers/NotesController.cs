using ExamRoomPOC.Domain.Interfaces.Services;
using ExamRoomPOC.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamRoomPOC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        public readonly INotesService _notesService;

        public NotesController(INotesService notesService)
        {
            _notesService = notesService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Note>>> GetAllNotes()
        {
            var notes = await _notesService.GetAll();

            if (notes != null && notes.Any())
            {
                return Ok(notes);
            }

            return NotFound("Any notes founded");
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNoteById(int id)
        {
            var note = await _notesService.GetById(id);

            if (note?.Id != null && !string.IsNullOrEmpty(note.Title))
            {
                return Ok(note);
            }

            return NotFound("Note not founded");
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateNote(Note note)
        {
            if (!string.IsNullOrEmpty(note.Title) && note?.Description != null)
            {
                _notesService.Create(note);

                return Ok();
            }

            return BadRequest("Note invalid to create");
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> ModifyNote(Note noteToUpdate, int id)
        {
            var noteFromDb = await _notesService.GetById(id);

            if (noteFromDb.Id > 0 && !string.IsNullOrEmpty(noteFromDb.Title))
            {
                _notesService.Modify(noteToUpdate, id);

                return Ok();
            }

            return NotFound("Note not found to update");
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Note>> ExcludeNote(int id)
        {
            var noteFromDb = await _notesService.GetById(id);

            if (noteFromDb.Id > 0 && !string.IsNullOrEmpty(noteFromDb.Title))
            {
                _notesService.Exclude(id);

                return Ok();
            }

            return NotFound("Note not found to delete");
        }
    }
}
