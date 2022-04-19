using Back3.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back3.Models.Request
{
    public class manageStockQ
    {
        private readonly CoffeeV2Context _context = new CoffeeV2Context();
         
        public async Task<object> GetManageStock()
        {
            return await _context.ManageStock.ToListAsync();
        }

        public async Task<object> GetManageStockByPdId(string id)
        {
            return await _context.ManageStock
                .Where(e => e.ProdcutId.Equals(id)) 
                    .ToListAsync();
        }

        public async Task<object> PostManageProduct(string productid, int amount, string type, [FromForm] string detail)
        {
            if (productid == null || type == null) return new { msg = "ข้อมูฃผิดพลาด" };

            var _data = new ManageStock
            {
                ProdcutId = productid,
                Date = DateTime.Now,
                Stock = amount,
                TypeManage = type,
                Detail = detail == null ? null : detail
            };
            _context.ManageStock.Add(_data);

            var pd = await _context.Product.AsNoTracking().FirstOrDefaultAsync(a => a.Id.Equals(productid));
            if (pd == null) return new { msg = "ไม่พบข้อมูลสินค้าในการจัดการคลัง" };
            
            if (type == "0")
            {
                pd.Stock = pd.Stock + amount;
                pd.StockSell = pd.StockSell - amount;
            }
            if (type == "1")
            {
                pd.Stock = pd.Stock - amount;
                pd.StockSell = pd.StockSell + amount;
            }
            else if (type == "2") pd.Stock = pd.Stock + amount;
            else pd.Stock = pd.Stock - amount;
             
            if(pd.Stock<0) return new { msg = "เกิดข้อผิดพลาด" };
            _context.Entry(pd).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return new { msg = "OK", data = _data };
        }

    }
}
