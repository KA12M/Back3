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
    public class paymentQ
    {
        private readonly CoffeeV2Context _context = new CoffeeV2Context(); 

        public async Task<object> GetPayment()
        {
            return await _context.Payment.ToListAsync();
        }
         
        public async Task<object> GetPayment(int id)
        {
            var payment = await _context.Payment.FindAsync(id);
            if (payment == null) return new { msg = "ไม่พบ" };
            return payment;
        }
         
        public async Task<object> PostPayment(String orderid, [FromForm] IFormFile Upfile)
        {
            #region ImageManageMent
            //var path = _environment.WebRootPath + Constants.Directory;
            // if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            //    //ตัดเอาเฉพาะชื่อไฟล์
            //    var fileName = "GG" + Constants.Image;
            //    if (Upfile.FileName != null)
            //        fileName += Upfile.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault();

            //    using (FileStream filestream = System.IO.File.Create(path + fileName))
            //    {
            //        Upfile.CopyTo(filestream);
            //        filestream.Flush();

            //        data.PayImg = Constants.Directory + fileName;
            //    }   
            #endregion

            if (orderid == null || Upfile == null) return new { msg = "ไม่พบ"};
            var data = new Payment();
            data.OrderId = orderid;
            data.Date = DateTime.Now; 

            String filename = "";
            if (Upfile != null)
            {
                string WepRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads\\");
                string uploads = Path.Combine(WepRootPath);
                if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
                //filename = Guid.NewGuid().ToString() + "." + Upfile.ContentType.Split('/')[1];
                filename = Guid.NewGuid().ToString() + "." + "jpeg";
                var fileSteam = new FileStream(Path.Combine(uploads, filename), FileMode.Create);
                await Upfile.CopyToAsync(fileSteam);
            }  
            data.PayImg = Constants.Directory + filename; 

            _context.Payment.Add(data);
            await _context.SaveChangesAsync();
            return new { msg = "OK", data };
        }
    }
}
