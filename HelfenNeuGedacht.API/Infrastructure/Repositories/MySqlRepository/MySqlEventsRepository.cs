using HelfenNeuGedacht.API.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HelfenNeuGedacht.API.Infrastructure.Repositories.MySqlRepository
{
    public class MySqlEventsRepository : IEventRepository
    {
        private readonly MySqlDbContext _context;

        public MySqlEventsRepository(MySqlDbContext context)
        {
            _context=context;
        }

        public async Task<Event> AddAsync(Event entity)
        {
            _context.Event.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Event> DeleteAsync(Event entity)
        {
            _context.Event.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Event>> FindAllAsync()
        {
            return await _context.Event.ToListAsync();
        }

        public async Task<Event?> FindByIdAsync(int id)
        {
            return await _context.Event.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Event> UpdateAsync(Event entity)
        {
            _context.Event.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Event?> FindByIdWithShiftsAsync(int id)
        {
            return await _context.Event
                .Include(e => e.Shift)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Event>> FindByOrganizationIdAsync(int organizationId)
        {
            return await _context.Event
                .Include(e => e.Shift)
                .Where(e => e.OrganizationId == organizationId)
                .ToListAsync();
        }
    }
}
