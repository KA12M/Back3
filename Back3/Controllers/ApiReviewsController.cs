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
    public class ApiReviewsController : ControllerBase
    {
        private readonly reviewQ _reviewQ = new reviewQ();
         
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReview()
        {
            try
            {
                return Ok(await _reviewQ.GetReview());
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            try
            {
                return Ok(await _reviewQ.GetReview(id));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }

        }

        [HttpGet("ByUserId/{id}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviewByUserId(int id)
        {
            try
            {
                return Ok(await _reviewQ.GetReviewByUserId(id));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }

        }

        [HttpPost]
        public async Task<ActionResult<Review>> PostReview([FromForm] Review data)
        {
            try
            {
                return Ok(await _reviewQ.PostReview(data));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Address>> DeleteReview(int id)
        {
            try
            {
                return Ok(await _reviewQ.DeleteReview(id));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }

        }


    }
}
