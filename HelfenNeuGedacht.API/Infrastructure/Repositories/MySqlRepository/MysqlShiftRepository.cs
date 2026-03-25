using HelfenNeuGedacht.API.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using HelfenNeuGedacht.API.Domain.Entities;

namespace HelfenNeuGedacht.API.Infrastructure.Repositories.MySqlRepository
{
    public class MysqlShiftRepository : IShiftRepository
    {
        private readonly MySqlDbContext _context;

        public MysqlShiftRepository(MySqlDbContext context)
        {
            _context = context;
        }

        public async Task<Shift> AddAsync(Shift entity)
        {
           _context.Shift.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Shift> DeleteAsync(Shift entity)
        {
            _context.Shift.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Shift>> FindAllAsync()
        {
            //throw new NotImplementedException();
            return await _context.Shift.ToListAsync();
        }

        public async Task<Shift?> FindByIdAsync(int id)
        {
          return await _context.Shift.FindAsync(id);

        }

        public async Task<Shift> UpdateAsync(Shift entity)
        {
            _context.Shift.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
