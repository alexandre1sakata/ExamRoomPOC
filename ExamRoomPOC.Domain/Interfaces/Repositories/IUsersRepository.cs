using ExamRoomPOC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamRoomPOC.Domain.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        User SelectById(int id);
        User SelectByInfo(string username, string password);
        void Insert(User user);
        void Delete(int id);
    }
}
