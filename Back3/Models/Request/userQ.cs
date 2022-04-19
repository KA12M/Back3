using Back3.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back3.Models.Request
{
    public class userQ
    {
        private readonly CoffeeV2Context _context = new CoffeeV2Context();

        public object GetUserWithSize(int currentPage, int pageSize, string search)
        {
            var query = _context.User.Where(e => e.Isused == "1" || e.Isused == "2");
            if (!string.IsNullOrEmpty(search)) query = query.Where(a => a.Name.Contains(search)||a.Email.Contains(search));

            int count = query.Count();
            var item = query.Skip((currentPage-1)*pageSize).Take(pageSize).ToList();

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

        public async Task<object> GetUsers()
        {
            return await _context.User.Where(a => a.Isused == "1" || a.Isused == "2").ToListAsync();
        }

        public async Task<object> Login([FromForm] User data)
        {
            var result = await _context.User.FirstOrDefaultAsync(p => p.Email.Equals(data.Email) && p.Password.Equals(data.Password) && p.Isused.Equals("1"));
            if (result == null) return new { msg = "อีเมลผู้ใช้หรือรหัสผ่านไม่ถูกต้อง" };

            return new { msg = "OK", data = new { result.Id,result.Name,result.Email,result.Status,result.DateReg } };
        }

        public async Task<object> Register([FromForm] User data)
        {
            var result = await _context.User.FirstOrDefaultAsync(p => p.Email.Equals(data.Email));
            if (result != null) return new { msg = "อีเมลผู้ใช้ซ้ำ" };

            data.Isused = "1";
            data.DateReg = DateTime.Now;
            if (data.Status == null) data.Status = "user";
            await _context.User.AddAsync(data);
            await _context.SaveChangesAsync();

            return new { msg = "OK", data };
        }

        public async Task<object> GetUser(int id)
        {
            return await _context.User.Where(User => User.Id == id && User.Isused == "1")
                .Include(e => e.Address).FirstAsync();
        }

        public async Task<object> PutUser([FromForm] User data)
        {
            var result = await _context.User.AsNoTracking().FirstOrDefaultAsync(p => p.Id.Equals(data.Id));
            if (result == null) return new { msg = "ไม่พบ" };

            data.Status = result.Status;
            data.DateReg = result.DateReg;
            _context.User.Update(data);
            await _context.SaveChangesAsync();

            return new { msg = "OK", data };
        }

        public async Task<object> deleteUser(int id,string del)
        {
            var result = await _context.User.AsNoTracking().FirstOrDefaultAsync(p => p.Id.Equals(id));
            if (del != null || del == "del") result.Isused = "0";
            else result.Isused = "2";

            _context.User.Update(result);
            await _context.SaveChangesAsync();
            return new { msg = "OK", data = result };
        } 

    }
}
