using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back3.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back3.Models.Request
{
    public class reviewQ
    {
        private readonly CoffeeV2Context _context = new CoffeeV2Context();
         
        public async Task<object> GetReview()
        {
            return await _context.Review.ToListAsync();
        }
         
        public async Task<object> GetReview(int id)
        {
            var review = await _context.Review.FindAsync(id);

            if (review == null) return new { msg = "ไม่พบ" }; 
            return review;
        }
         
        public async Task<object> GetReviewByUserId(int id)
        {
            var data = await _context.Review.Where(p => p.UserId.Equals(id))
                .Include(e => e.Product)
                    .ThenInclude(e => e.ProductDetail)
                .ToListAsync();

            if (data == null) return new { msg = "ไม่พบ" }; 
            return new { msg = "OK", data };
        }
         
        public async Task<object> PostReview([FromForm] Review data)
        {
            data.Date = DateTime.Now;
            _context.Review.Add(data);
            await _context.SaveChangesAsync();
            return new { msg = "OK", data };
        }
         
        public async Task<object> DeleteReview(int id)
        {
            var data = await _context.Review.FindAsync(id);
            if (data == null) return new { msg = "ไม่พบ" }; 
            _context.Review.Remove(data);
            await _context.SaveChangesAsync();
            return new { msg = "OK", data };
        }

    }
}
