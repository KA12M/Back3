using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back3.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back3.Models.Request
{
    public class productQ
    {
        private readonly CoffeeV2Context _context = new CoffeeV2Context();
         
        public async Task<object> GetProduct()
        {
            var data = await _context.Product.Where(e => e.Isused == "1").Include(e => e.ProductDetail).ToListAsync();
            return new { msg = "OK", data };
        }

        public object GetProductWithSize(int currentPage, int pageSize, string search)
        {
            var query = _context.Product.Where(e => e.Isused == "1");
            if (!string.IsNullOrEmpty(search)) query = query.Where(a => a.Name.Contains(search));

            int count = query.Count();
            var item = query.Skip((currentPage - 1) * pageSize).Take(pageSize).Include(e => e.ProductDetail).Include(e => e.Type).ToList();

            return new
            {
                StatusCode = count == 0 ? "000" : "001",
                Message = count == 0 ? "ไม่พบข้อมูล" : "พบข้อมูล", 
                Pagin = new
                {
                    currentPage,
                    pageSize,
                    totalPage = (int)Math.Ceiling((double)count / pageSize),
                    totalRow = count,
                },
                data = item
            }; 
        }

        public async Task<object> GetProductInclude()
        {
            var data = await _context.Product.Where(e => e.Isused == "1")
                .Include(e => e.Type)
                .Include(e => e.ProductDetail) 
                .Include(e => e.Review)
                    .ThenInclude(e => e.ReviewDetail)
                .Include(e => e.Review)
                    .ThenInclude(e => e.User)  
                .ToListAsync();
            return new { msg = "OK", data };
        }
         
        public async Task<object> GetProduct(string id)
        {
            var data = await _context.Product.Where(p => p.Id.Equals(id) && p.Isused == "1")
                .Include(e => e.ProductDetail)
                .Include(e => e.Type)
                .Include(e => e.ManageStock)
                .Include(e => e.Review)
                    .ThenInclude(e => e.User)
                .Include(e => e.Review)
                    .ThenInclude(e => e.ReviewDetail)
                .ToListAsync();

            if (data == null) return new { msg = "ไม่พบ" }; 
            return new { msg = "OK", data };
        }
         
        public async Task<object> PostProduct([FromForm] Product data)
        {
            data.Id = Guid.NewGuid().ToString("N").Substring(0, 12);
            data.Stock = 0;
            data.StockSell = 0;
            data.Isused = "1";
            _context.Product.Add(data);
            await _context.SaveChangesAsync();
            return new { msg = "OK", data };
        }
         
        public async Task<object> chkStock(string pdid, int amount)
        {
            var result = await _context.Product.FirstOrDefaultAsync(p => p.Id.Equals(pdid));
            if (result == null) return new { msg = "ไม่พบสินค้า" };
            if (amount > result.Stock) return new { msg = "dont" };
            else return new { msg = "have" };
        }
         
        public async Task<object> PutProduct([FromForm] Product data)
        {
            var result = await _context.Product.AsNoTracking().FirstOrDefaultAsync(p => p.Id.Equals(data.Id));
            if (result == null)return new { msg = "ไม่พบ" };
            _context.Product.Update(data);
            await _context.SaveChangesAsync(); 
            return new { msg = "OK", data };
        }
         
        public async Task<object> deleteProduct(string id)
        {
            var result = await _context.Product.AsNoTracking().FirstOrDefaultAsync(p => p.Id.Equals(id));
            result.Isused = "0";
            result.Name = result.Name + "/รายการที่ลบ";
            _context.Product.Update(result);
            await _context.SaveChangesAsync();
            return new { msg = "OK", data = result };
        }
    }
}
