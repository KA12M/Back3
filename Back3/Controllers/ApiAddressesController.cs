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
    public class ApiAddressesController : ControllerBase
    {  
        private readonly addressQ addressQ = new addressQ();

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddress()
        {
            try
            {
                return Ok(await addressQ.GetAddress());
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddressByUserId(int id)
        {
            try
            {
                return Ok(await addressQ.GetAddressByUserId(id));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress([FromForm] Address address)
        {
            try
            {
                return Ok(await addressQ.PostAddress(address));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Address>>> DeleteAddress(int id)
        {
            try
            {
                return Ok(await addressQ.DeleteAddress(id));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

    }
}
