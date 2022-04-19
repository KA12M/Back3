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
    public class ApiTypeProductsController : ControllerBase
    {
        private readonly typeProductQ _typeProductQ = new typeProductQ();

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeProduct>>> GetTypeProduct()
        {
            try
            {
                return Ok(await _typeProductQ.GetTypeProduct());
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }
         
        [HttpGet("include")]
        public async Task<ActionResult<IEnumerable<TypeProduct>>> GetTypeProductInclude()
        {
            try
            {
                return Ok(await _typeProductQ.GetTypeProductInclude());
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TypeProduct>> GetTypeProduct(int id)
        {
            try
            {
                return Ok(await _typeProductQ.GetTypeProduct(id));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutTypeProduct([FromForm] TypeProduct data)
        {
            try
            {
                return Ok(await _typeProductQ.PutTypeProduct(data));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<TypeProduct>> PostTypeProduct([FromForm] TypeProduct data)
        {
            try
            {
                return Ok(await _typeProductQ.PostTypeProduct(data));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TypeProduct>> DeleteTypeProduct(int id)
        {
            try
            {
                return Ok(await _typeProductQ.DeleteTypeProduct(id));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

    }
}
