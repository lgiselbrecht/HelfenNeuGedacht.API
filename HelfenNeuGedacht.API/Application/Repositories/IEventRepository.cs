namespace HelfenNeuGedacht.API.Application.Repositories
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<Event?> FindByIdWithShiftsAsync(int id);
        Task<List<Event>> FindByOrganizationIdAsync(int organizationId);
    }
}
