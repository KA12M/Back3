using Back3.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back3.Models.Request
{
    public class orderDetailQ
    {
        private readonly CoffeeV2Context _context = new CoffeeV2Context();
         
        public async Task<object> GetOrderDetail()
        {
            return await _context.OrderDetail
                .Include(e => e.Product)
                .ToListAsync();
        }
         
        public async Task<object> GetOrderDetailByOrderId(String id)
        {
            var data = await _context.OrderDetail.Where(p => p.OrderId.Equals(id))
                .Include(e => e.Product)
                .ToListAsync();
            if (data == null) return new { msg = "ไม่พบ" }; 
            return new { msg = "OK", data };
        }
         
        public async Task<object> PostOrderDetail([FromForm] OrderDetail data)
        {
            _context.OrderDetail.Add(data);
            await _context.SaveChangesAsync();
            return new { msg = "OK", data };
        }
    }
}
