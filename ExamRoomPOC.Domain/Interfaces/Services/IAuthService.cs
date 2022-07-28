using ExamRoomPOC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamRoomPOC.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        string GenerateToken(User user);
    }
}
