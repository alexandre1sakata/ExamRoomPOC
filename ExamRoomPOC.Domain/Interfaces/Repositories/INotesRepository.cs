using ExamRoomPOC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamRoomPOC.Domain.Interfaces.Repositories
{
    public interface INotesRepository
    {
        Task<List<Note>> SelectAll();
        Task<Note> SelectOne(int id);
        void Insert(Note note);
        Task<Note> Update(Note note, int id);
        void Delete(int id);
    }
}
