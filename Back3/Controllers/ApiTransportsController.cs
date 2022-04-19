using Back3.Models.Data;
using Back3.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiTransportsController : ControllerBase
    {
        private readonly transportQ _transportQ = new transportQ();

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransportDetail>>> GetTransport()
        {
            try
            {
                return Ok(await _transportQ.GetTransport());
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            } 
        }

        [HttpPost]
        public async Task<ActionResult<TransportDetail>> PostTransport([FromForm] TransportDetail data)
        {
            try
            {
                return Ok(await _transportQ.PostTransport(data));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TransportDetail>> DeleteTransport(int id)
        {
            try
            {
                return Ok(await _transportQ.DeleteTransport(id));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }


    }
}
