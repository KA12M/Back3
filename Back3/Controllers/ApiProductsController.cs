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
    public class ApiProductsController : ControllerBase
    {
        private readonly productQ _productQ = new productQ();

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            try
            {
                return Ok(await _productQ.GetProduct());
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpGet("withsize")]
        public ActionResult<IEnumerable<Product>> GetProductWithSize(int currentPage = 1, int pageSize = 10, string search = "")
        {
            try
            {
                return Ok(_productQ.GetProductWithSize(currentPage, pageSize, search));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpGet("include")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductInclude()
        {
            try
            {
                return Ok(await _productQ.GetProductInclude());
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct(string id)
        {
            try
            {
                return Ok(await _productQ.GetProduct(id));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromForm] Product data)
        {
            try
            {
                return Ok(await _productQ.PostProduct(data));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpPost("chkStock/{pdid}/{amount}")]
        public async Task<object> chkStock(string pdid,int amount)
        {
            try
            {
                return Ok(await _productQ.chkStock(pdid,amount));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        } 

        [HttpPut]
        public async Task<ActionResult<Product>> PutProduct([FromForm] Product data)
        {
            try
            {
                return Ok(await _productQ.PutProduct(data));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> deleteProduct(string id)
        {
            try
            {
                return Ok(await _productQ.deleteProduct(id));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

    }
}
