using SalesWebMVC.Data;
using System.Collections.Generic;
using System.Linq;
using SalesWebMVC.Models;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Services.Exceptions;
using System.Threading.Tasks;

namespace SalesWebMVC.Services
{
    public class SellerService
    {
        private readonly SalesWebMVCContext _context;

        public SellerService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int Id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(s => s.Id == Id);
        }

        public async Task RemoveAsync(int Id)
        {
            var seller = await _context.Seller.FindAsync(Id);
            _context.Seller.Remove(seller);
            await _context.SaveChangesAsync();
        }

        public async Task UpDateAsync(Seller seller)
        {
            bool noSeller = !(await _context.Seller.AnyAsync(x => x.Id == seller.Id));
            if (noSeller)
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(seller);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
            
        }
    }
}
