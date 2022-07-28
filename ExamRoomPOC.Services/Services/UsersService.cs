using ExamRoomPOC.Domain.Interfaces.Repositories;
using ExamRoomPOC.Domain.Interfaces.Services;
using ExamRoomPOC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamRoomPOC.Services.Services
{
    public class UsersService : IUsersService
    {
        public readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public User GetById(int id)
        {
            return _usersRepository.SelectById(id);
        }

        public User GetByInfo(string userName, string password)
        {
            return _usersRepository.SelectByInfo(userName, password);
        }

        public void Create(User user)
        {
            _usersRepository.Insert(user);
        }

        public void Exclude(int id)
        {
            _usersRepository.Delete(id);
        }
    }
}
