using ExamRoomPOC.Domain.Interfaces.Repositories;
using ExamRoomPOC.Domain.Interfaces.Services;
using ExamRoomPOC.Domain.Models;
using ExamRoomPOC.Services.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamRoomPOC.Services.Services
{
    public class NotesService : INotesService
    {
        private readonly INotesRepository _notesRepository;
        private readonly IHubContext<HubNotes> _hubContext;

        public NotesService(INotesRepository notesRepository, IHubContext<HubNotes> hubContext)
        {
            _notesRepository = notesRepository;
            _hubContext = hubContext;
        }

        public async Task<List<Note>> GetAll()
        {
            return await _notesRepository.SelectAll();
        }

        public async Task<Note> GetById(int id)
        {
            return await _notesRepository.SelectOne(id);
        }

        public async void Create(Note note)
        {
            _notesRepository.Insert(note);

            await _hubContext.Clients.All.SendAsync("ReceiveNotesAction", HubNotesActions.Insert.ToString());
        }

        public async void Modify(Note note, int id)
        {
            await _notesRepository.Update(note, id);

            await _hubContext.Clients.All.SendAsync("ReceiveNotesAction", HubNotesActions.Update.ToString());
        }

        public async void Exclude(int id)
        {
            _notesRepository.Delete(id);

            await _hubContext.Clients.All.SendAsync("ReceiveNotesAction", HubNotesActions.Delete.ToString());
        }
    }
}
