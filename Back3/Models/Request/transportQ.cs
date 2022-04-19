using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back3.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back3.Models.Request
{
    public class transportQ
    {
        private readonly CoffeeV2Context _context = new CoffeeV2Context();
         
        public async Task<object> GetTransport()
        {
            return await _context.TransportDetail.ToListAsync();
        }
         
        public async Task<object> PostTransport([FromForm] TransportDetail data)
        {
            data.Date = DateTime.Now;
            _context.TransportDetail.Add(data);
            await _context.SaveChangesAsync();

            return new { msg = "OK", data };
        }
         
        public async Task<object> DeleteTransport(int id)
        {
            var transportDetail = await _context.TransportDetail.FindAsync(id);
            if (transportDetail == null) return new { msg = "ไม่พบ" };

            _context.TransportDetail.Remove(transportDetail);
            await _context.SaveChangesAsync();

            return transportDetail;
        }

    }
}
