using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ExamRoomPOC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamRoomPOC.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace ExamRoomPOC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public readonly IAuthService _authService;
        public readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        public ActionResult CreateUser(User user)
        {
            _usersService.Create(user);

            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _usersService.GetById(id);

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public ActionResult ExcludeUser(int id)
        {
            _usersService.Exclude(id);

            return Ok();
        }
    }
}
