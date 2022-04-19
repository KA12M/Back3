using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Back3.Models.Data;
using Backend.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back3.Models.Request
{
    public class reviewDetailQ
    {
        private readonly CoffeeV2Context _context = new CoffeeV2Context(); 
         
        public async Task<object> GetReviewDetail()
        {
            return await _context.ReviewDetail.ToListAsync();
        }
         
        public async Task<object> PostReviewDetail([FromForm] ReviewDetail data, [FromForm] IFormFileCollection Upfile)
        {
            foreach (var item in Upfile)
            {
                #region ImageManageMent
                String filename = "";
                if (Upfile != null)
                {
                    string WepRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads\\");
                    string uploads = Path.Combine(WepRootPath);
                    if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
                    //filename = Guid.NewGuid().ToString() + "." + item.ContentType.Split('/')[1];
                    filename = Guid.NewGuid().ToString() + "." + "jpeg";
                    var fileSteam = new FileStream(Path.Combine(uploads, filename), FileMode.Create);
                    await item.CopyToAsync(fileSteam);
                } 
                #endregion 
                data.Id = data.ReviewId.ToString() + DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss-fffffff");
                data.Img = Constants.Directory + filename;
                _context.ReviewDetail.Add(data);
                await _context.SaveChangesAsync();
            }

            return new { msg = "OK", data };
        }

        [HttpDelete("{id}")]
        public async Task<object> DeleteReviewDetailByReviewId(int id)
        {
            var dataList = await _context.ReviewDetail.Where(p => p.ReviewId.Equals(id)).ToListAsync();
            foreach (var item in dataList)
            {
                var path = Constants.Directory + item.Img;
                if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                _context.ReviewDetail.Remove(item);
                await _context.SaveChangesAsync();
            }
            return new { msg = "OK", dataList };
        }
    }
}
