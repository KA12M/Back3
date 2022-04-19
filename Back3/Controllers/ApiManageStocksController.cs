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
    public class ApiManageStocksController : ControllerBase
    {
        private readonly manageStockQ _manageStockQ = new manageStockQ();

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ManageStock>>> GetManageStock()
        {
            try
            {
                return Ok(await _manageStockQ.GetManageStock());
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpGet("bypdid/{id}")]
        public async Task<ActionResult<IEnumerable<ManageStock>>> GetManageStockByPdId(string id)
        {
            try
            {
                return Ok(await _manageStockQ.GetManageStockByPdId(id));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpPost("postmanage/{productid}/{amount}/{type}")]
        public async Task<ActionResult<ManageStock>> PostManageProduct(string productid, int amount, string type, [FromForm] string detail)
        {
            try
            {
                return Ok(await _manageStockQ.PostManageProduct(productid,amount,type,detail));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

    }
}
