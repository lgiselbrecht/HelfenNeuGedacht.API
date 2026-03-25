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

        public async Task<HelpingEvents> AddAsync(HelpingEvents entity)
        {
            _context.Event.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<HelpingEvents> DeleteAsync(HelpingEvents entity)
        {
            _context.Event.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<HelpingEvents>> FindAllAsync()
        {
            return await _context.Event.ToListAsync();
        }

        public async Task<HelpingEvents?> FindByIdAsync(int id)
        {
            return await _context.Event.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<HelpingEvents> UpdateAsync(HelpingEvents entity)
        {
            _context.Event.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<HelpingEvents?> FindByIdWithShiftsAsync(int id)
        {
            return await _context.Event
                .Include(e => e.Shift)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<HelpingEvents>> FindByOrganizationIdAsync(int organizationId)
        {
            return await _context.Event
                .Where(e => e.OrganizationId == organizationId)
                .ToListAsync();
        }
    }
}
