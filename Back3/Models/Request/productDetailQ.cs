using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Back3.Models.Data;
using Backend.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back3.Models.Request
{
    public class productDetailQ
    {
        private readonly CoffeeV2Context _context = new CoffeeV2Context();
         
        public async Task<object> GetProductDetail()
        {
            return await _context.ProductDetail.ToListAsync();
        }
         
        public async Task<object> GetProductDetailByProductId(int id)
        {
            var data = await _context.ProductDetail.Where(p => p.ProductId.Equals(id)).ToListAsync(); 
            if (data == null) return new { msg = "ไม่พบ" };
            return new { msg = "OK", data };
        }

        public async Task<object> PostProductDetail([FromForm] ProductDetail data, [FromForm] IFormFile Upfile)
        {
            if (Upfile != null)
            {
                #region ImageManageMent
                String filename = "";
                string WepRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads\\");
                string uploads = Path.Combine(WepRootPath);
                if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
                filename = Guid.NewGuid().ToString() + "." + Upfile.ContentType.Split('/')[1];
                var fileSteam = new FileStream(Path.Combine(uploads, filename), FileMode.Create);
                await Upfile.CopyToAsync(fileSteam);
                #endregion
                data.Img = Constants.Directory + filename;
                data.Id = data.ProductId + DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss-fffffff");
                _context.ProductDetail.Add(data);
                await _context.SaveChangesAsync();
            }

            return new { msg = "OK", data };
        }

        public async Task<object> PostProductDetails([FromForm] ProductDetail data, [FromForm] IFormFileCollection Upfile)
        {
            if (Upfile != null)
            foreach (var item in Upfile)
            {
                #region ImageManageMent
                String filename = ""; 
                    string WepRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads\\");
                    string uploads = Path.Combine(WepRootPath);
                    if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
                    filename = Guid.NewGuid().ToString() + "." + item.ContentType.Split('/')[1];
                    var fileSteam = new FileStream(Path.Combine(uploads, filename), FileMode.Create);
                    await item.CopyToAsync(fileSteam); 
                #endregion
                data.Img = Constants.Directory + filename;
                data.Id = data.ProductId + DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss-fffffff");
                _context.ProductDetail.Add(data);
                await _context.SaveChangesAsync();
            }

            return new { msg = "OK", data };
        }
         
        public async Task<object> DeleteProductDetail(string id)
        {
            var data = await _context.ProductDetail.FindAsync(id);
            var path = Constants.Directory + data.Img;
            if (System.IO.File.Exists(path)) System.IO.File.Delete(path); 
            _context.ProductDetail.Remove(data);
            await _context.SaveChangesAsync(); 

            return new { msg = "OK", data };
        }
         
        public async Task<object> PutProductDetail([FromForm] ProductDetail data)
        {
            if (_context.ProductDetail.FindAsync(data.Id) == null) return new { msg = "ไม่พบ" };
            _context.ProductDetail.Update(data);
            await _context.SaveChangesAsync();
            return new { msg = "OK", data };
        }
    }
}
