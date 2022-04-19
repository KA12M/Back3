using Back3.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back3.Models.Request
{
    public class addressQ
    {
        private readonly CoffeeV2Context _context = new CoffeeV2Context();

        public async Task<object> GetAddress()
        {
            return await _context.Address.ToListAsync();
        }

        public async Task<object> GetAddressByUserId(int id)
        {
            var result = await _context.Address.Where(Address => Address.UserId == id && Address.Isused == "1").ToListAsync();
            return new { msg = "OK", data = result };
        }

        public async Task<object> PostAddress([FromForm] Address address)
        {
            address.Isused = "1";
            _context.Address.Add(address);
            await _context.SaveChangesAsync();

            return new { msg = "OK", data = address };
        }

        public async Task<object> DeleteAddress(int id)
        {
            var address = await _context.Address.FindAsync(id);  
            if(address==null) return new { msg = "ไม่พบ"};
            address.Isused = "0";
            _context.Address.Update(address);
            await _context.SaveChangesAsync();

            return new { msg = "OK", data = address };
        }
          
    }
}
