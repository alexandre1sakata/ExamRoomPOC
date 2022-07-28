using ExamRoomPOC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamRoomPOC.Domain.Interfaces.Services
{
    public interface INotesService
    {
        Task<List<Note>> GetAll();
        Task<Note> GetById(int id);
        void Create(Note note);
        void Modify(Note note, int id);
        void Exclude(int id);
    }
}
