using Back3.Models.Data;
using Back3.Models.Request;
using Backend.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Back3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiPaymentsController : ControllerBase
    {  
        private readonly paymentQ _paymentQ = new paymentQ();

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayment()
        {
            try
            {
                return Ok(await _paymentQ.GetPayment());
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(int id)
        {
            try
            {
                return Ok(await _paymentQ.GetPayment(id));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpPost("{orderid}")]
        public async Task<ActionResult<Payment>> PostPayment(String orderid, [FromForm] IFormFile Upfile)
        {
            try
            {
                return Ok(await _paymentQ.PostPayment(orderid, Upfile));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }


    }
}
