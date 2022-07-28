using ExamRoomPOC.Domain.Interfaces.Services;
using ExamRoomPOC.Domain.Models;
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
    public class AuthController : ControllerBase
    {
        public readonly IAuthService _authService;
        public readonly IUsersService _usersService;

        public AuthController(IAuthService authService, IUsersService usersService)
        {
            _authService = authService;
            _usersService = usersService;
        }

        [HttpPost("login")]
        public ActionResult<UserToken> Login(User user)
        {
            if (user?.Username != null || user?.Password != null)
            {
                var userToAuth = _usersService.GetByInfo(user.Username, user.Password);

                if (userToAuth?.Username != null && userToAuth?.Password != null)
                {
                    var token = new UserToken { Token = _authService.GenerateToken(userToAuth) };

                    return Ok(token);
                }
                else
                    return BadRequest("Invalid credentials!");
            }
            else
                return NotFound();
        }
    }
}
