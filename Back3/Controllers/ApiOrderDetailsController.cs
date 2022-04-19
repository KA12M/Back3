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
    public class ApiOrderDetailsController : ControllerBase
    {
        private readonly orderDetailQ _orderDetailQ = new orderDetailQ();

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetail()
        {
            try
            {
                return Ok(await _orderDetailQ.GetOrderDetail());
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }
          
        [HttpGet("ByOrderId/{id}")]
        public async Task<ActionResult<OrderDetail>> GetOrderDetailByOrderId(String id)
        {
            try
            {
                return Ok(await _orderDetailQ.GetOrderDetailByOrderId(id));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<OrderDetail>> PostOrderDetail([FromForm] OrderDetail data)
        {
            try
            {
                return Ok(await _orderDetailQ.PostOrderDetail(data));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }
         
    }
}
