using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back3.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back3.Models.Request
{
    public class typeProductQ
    {
        private readonly CoffeeV2Context _context = new CoffeeV2Context();
         
        public async Task<object> GetTypeProduct()
        {
            return await _context.TypeProduct.ToListAsync();
        }
         
        public async Task<object> GetTypeProductInclude()
        {
            return await _context.TypeProduct
                .Include(e => e.Product)
                    .ThenInclude(e => e.ProductDetail)
                .ToListAsync();
        }
         
        public async Task<object> GetTypeProduct(int id)
        {
            var typeProduct = await _context.TypeProduct.FindAsync(id);
            if (typeProduct == null) return new { msg = "ไม่พบ" };

            return typeProduct;
        }
         
        public async Task<object> PutTypeProduct([FromForm] TypeProduct data)
        {
            if (_context.TypeProduct.FindAsync(data.Id) == null) return new { msg = "ไม่พบ" };
            _context.TypeProduct.Update(data);
            await _context.SaveChangesAsync();
            return new { msg = "OK", data }; 
        }
         
        public async Task<object> PostTypeProduct([FromForm] TypeProduct data)
        {
            _context.TypeProduct.Add(data);
            await _context.SaveChangesAsync(); 
            return new { msg = "OK", data = data };
        }
         
        public async Task<object> DeleteTypeProduct(int id)
        {
            var typeProduct = await _context.TypeProduct.FindAsync(id);
            if (typeProduct == null) return new { msg = "ไม่พบ" };

            _context.TypeProduct.Remove(typeProduct);
            await _context.SaveChangesAsync(); 
            return typeProduct;
        }

    }
}
