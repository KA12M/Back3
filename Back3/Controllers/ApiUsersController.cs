using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Back3.Models.Data;
using Back3.Models.Request;

namespace Back3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiUsersController : ControllerBase
    {
        private readonly userQ userQ = new userQ();
        
        [HttpGet("withSize/{currentPage}/{pageSize}")]
        public ActionResult<IEnumerable<User>> GetUserWithSize(int currentPage = 1, int pageSize = 10, string search = "")
        {
            try
            {
                return Ok(userQ.GetUserWithSize(currentPage, pageSize, search));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUserAll()
        {
            try
            {
                return Ok(await userQ.GetUsers());
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            } 
        }
         
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromForm] User data)
        {
            try
            {
                return Ok(await userQ.Login(data));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }
         
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromForm] User data)
        {
            try
            {
                return Ok(await userQ.Register(data));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                return Ok(await userQ.GetUser(id));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutUser([FromForm] User data)
        {
            try
            {
                return Ok(await userQ.PutUser(data));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> deleteUser(int id,string del)
        {
            try
            {
                return Ok(await userQ.deleteUser(id,del));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }


    }
}
