using HelfenNeuGedacht.API.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HelfenNeuGedacht.API.Infrastructure.Repositories.MySqlRepository
{
    public class MysqlOrganizationRepository : IOrganizationRepository
    {
        private readonly MySqlDbContext _context;

        public MysqlOrganizationRepository(MySqlDbContext context)
        {
            _context = context;
        }

        public async Task<Organization> AddAsync(Organization entity)
        {
           _context.Organization.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Organization> DeleteAsync(Organization entity)
        {
            _context.Organization.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Organization>> FindAllAsync()
        {
            throw new NotImplementedException();
            // return await _context.Organization.ToListAsync();
        }

        public async Task<Organization?> FindByIdAsync(int id)
        {
          return await _context.Organization.FindAsync(id);

        }

        public async Task<Organization> UpdateAsync(Organization entity)
        {
            _context.Organization.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
