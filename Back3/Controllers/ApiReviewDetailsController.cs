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
    public class ApiReviewDetailsController : ControllerBase
    {
        private readonly reviewDetailQ _reviewDetailQ = new reviewDetailQ();

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDetail>>> GetReviewDetail()
        {
            try
            {
                return Ok(await _reviewDetailQ.GetReviewDetail());
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }

        }

        [HttpPost]
        public async Task<ActionResult<ReviewDetail>> PostReviewDetail([FromForm] ReviewDetail data, [FromForm] IFormFileCollection Upfile)
        {
            try
            {
                return Ok(await _reviewDetailQ.PostReviewDetail(data,Upfile));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ReviewDetail>> DeleteReviewDetailByReviewId(int id)
        {
            try
            {
                return Ok(await _reviewDetailQ.DeleteReviewDetailByReviewId(id));
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message = e.Message });
            }

        }
    }
}
