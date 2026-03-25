namespace HelfenNeuGedacht.API.Application.Repositories
{
    public interface IEventRepository : IRepository<HelpingEvents>
    {
        Task<HelpingEvents?> FindByIdWithShiftsAsync(int id);
        Task<List<HelpingEvents>> FindByOrganizationIdAsync(int organizationId);
    }
}
