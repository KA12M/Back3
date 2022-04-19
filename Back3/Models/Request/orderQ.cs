using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back3.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back3.Models.Request
{
    public class orderQ
    {
        private readonly CoffeeV2Context _context = new CoffeeV2Context();
         
        public async Task<object> GetOrders()
        {
            return await _context.Order.Where(e => e.Isused == "1") 
                .Include(e => e.OrderDetail)
                    .ThenInclude(e => e.Product)
                        .ThenInclude(e => e.ProductDetail)   
                .Include(e => e.TransportDetail)
                .ToListAsync();
        }
         
        public async Task<object> GetOrderByUserId(int id)
        {
            var data = await _context.Order.Where(p => p.UserId.Equals(id) && p.Isused == "1")
                .Include(e => e.Address)
                .Include(e => e.OrderDetail)
                    .ThenInclude(e => e.Product)
                        .ThenInclude(e => e.ProductDetail)
                .Include(e => e.OrderDetail)
                    .ThenInclude(e => e.Product)
                        .ThenInclude(e => e.Type)
                .Include(e => e.OrderDetail)
                    .ThenInclude(e => e.Product)
                        .ThenInclude(e => e.Review)
                .Include(e => e.Payment)
                .Include(e => e.TransportDetail)
                .ToListAsync();

            if (data == null) return new { msg = "ไม่พบ" }; 
            return new { msg = "OK", data };
        }
         
        public async Task<object> GetOrderById(String id)
        {
            var data = await _context.Order.Where(e => e.Id.Equals(id)).Include(e => e.Address)
                .Include(e => e.OrderDetail)
                    .ThenInclude(e => e.Product)
                        .ThenInclude(e => e.ProductDetail)
                .Include(e => e.OrderDetail)
                    .ThenInclude(e => e.Product)
                        .ThenInclude(e => e.Type)
                .Include(e => e.OrderDetail)
                    .ThenInclude(e => e.Product)
                        .ThenInclude(e => e.Review)
                .Include(e => e.Payment)
                .Include(e => e.TransportDetail)
                .ToListAsync(); ;

            if (data == null) return new { msg = "ไม่พบ" }; 
            return new { msg = "OK", data = data[0]};
        }
         
        public async Task<object> PostOrder([FromForm] Order data)
        {
            data.Isused = "1";
            data.Date = DateTime.Now;
            data.Id = Guid.NewGuid().ToString("N").Substring(0, 18);
            _context.Order.Add(data);
            await _context.SaveChangesAsync();
            return new { msg = "OK", data };
        }
         
        public async Task<object> PutUser([FromForm] Order order)
        {
            var result = await _context.Order.AsNoTracking().FirstOrDefaultAsync(p => p.Id.Equals(order.Id));
            if (result == null)return new { msg = "ไม่พบ" };

            _context.Order.Update(order);
            await _context.SaveChangesAsync();

            return new { msg = "OK", data = order };
        }
         
        public async Task<object> ConfirmOrder(String id)
        {
            var data = await _context.Order.FindAsync(id);
            if (data == null) return new { msg = "ไม่พบคำสั่งซื้อ" };

            data.Status = "การจัดส่ง";
            data.TransportCode = Guid.NewGuid().ToString("N").Substring(0, 12);
            _context.Order.Update(data);

            if (data.TransportDetail.Count == 0)
            {
                TransportDetail transport = new TransportDetail
                {
                    Status = "กำลังจัดเตรียมพัสดุ",
                    Date = DateTime.Now,
                    OrderId = id
                };
                _context.TransportDetail.Add(transport);
            } 
            await _context.SaveChangesAsync(); 
            return new { msg = "OK", data };
        }
         
        public async Task<object> setOrderSuccess(String id)
        {
            var result = await _context.Order.AsNoTracking().FirstOrDefaultAsync(p => p.Id.Equals(id));
            if (result == null) return new { msg = "ไม่พบ" };

            result.Status = "สำเร็จ";
            _context.Order.Update(result);
            await _context.SaveChangesAsync();

            return new { msg = "OK", data = result };
        }
         
        public async Task<object> deleteOrder(string id)
        {
            var result = await _context.Order.AsNoTracking().FirstOrDefaultAsync(p => p.Id.Equals(id));
            result.Isused = "0";
            _context.Order.Update(result);
            await _context.SaveChangesAsync();
            return new { msg = "OK", data = result };
        }
    }
}
