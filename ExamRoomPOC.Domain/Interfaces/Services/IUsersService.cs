using ExamRoomPOC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamRoomPOC.Domain.Interfaces.Services
{
    public interface IUsersService
    {
        User GetById(int id);
        User GetByInfo(string userName, string password);
        void Create(User user);
        void Exclude(int id);
    }
}
