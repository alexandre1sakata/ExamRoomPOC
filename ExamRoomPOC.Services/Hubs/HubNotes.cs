using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamRoomPOC.Services.Hubs
{
    public class HubNotes : Hub
    {
        private readonly IHubContext<HubNotes> _hubContext;
        public HubNotes(IHubContext<HubNotes> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendNoteAction(string action)
        {
            if (_hubContext.Clients != null)
                await _hubContext.Clients.All.SendAsync("ReceiveNotesAction", action);
        }
    }
}
