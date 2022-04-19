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
    public class ApiProductDetailsController : ControllerBase
    {
        private readonly productDetailQ _productDetailQ = new productDetailQ();

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDetail>>> GetProductDetail()
        {
            try
            {
                return Ok(await _productDetailQ.GetProductDetail());
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpGet("ByProductId/{id}")]
        public async Task<ActionResult<IEnumerable<ProductDetail>>> GetProductDetailByProductId(int id)
        {
            try
            {
                return Ok(await _productDetailQ.GetProductDetailByProductId(id));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProductDetail>> PostProductDetail([FromForm] ProductDetail data, [FromForm] IFormFile Upfile)
        {
            try
            {
                return Ok(await _productDetailQ.PostProductDetail(data, Upfile));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpPost("multiple")]
        public async Task<ActionResult<ProductDetail>> PostProductDetails([FromForm] ProductDetail data, [FromForm] IFormFileCollection Upfile)
        {
            try
            {
                return Ok(await _productDetailQ.PostProductDetails(data,Upfile));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductDetail>> DeleteProductDetail(string id)
        {
            try
            {
                return Ok(await _productDetailQ.DeleteProductDetail(id));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutProductDetail([FromForm] ProductDetail data)
        {
            try
            {
                return Ok(await _productDetailQ.PutProductDetail(data));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }
        }

    }
}
