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
    public class ApiOrdersController : ControllerBase
    {
        private readonly orderQ _orderQ = new orderQ();

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            try
            {
                return Ok(await _orderQ.GetOrders());
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }
         
        [HttpGet("ByUserId/{id}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrderByUserId(int id)
        {
            try
            {
                return Ok(await _orderQ.GetOrderByUserId(id));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }
         
        [HttpGet("ById/{id}")]
        public async Task<ActionResult<Order>> GetOrderById(String id)
        {
            try
            {
                return Ok(await _orderQ.GetOrderById(id));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder([FromForm] Order data)
        {
            try
            {
                return Ok(await _orderQ.PostOrder(data));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }
         
        [HttpPut]
        public async Task<IActionResult> PutUser([FromForm] Order order)
        {
            try
            {
                return Ok(await _orderQ.PutUser(order));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }
         
        [HttpPost("ConfirmOrder/{id}")]
        public async Task<ActionResult<Order>> ConfirmOrder(String id)
        {
            try
            {
                return Ok(await _orderQ.ConfirmOrder(id));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }
         
        [HttpPost("orderfinish/{id}")]
        public async Task<IActionResult> setOrderSuccess(String id)
        {
            try
            {
                return Ok(await _orderQ.setOrderSuccess(id));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> deleteOrder(string id)
        {
            try
            {
                return Ok(await _orderQ.deleteOrder(id));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }


    }
}
