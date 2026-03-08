using HelfenNeuGedacht.API.Domain.Entities;

namespace HelfenNeuGedacht.API.Application.Services.ShiftServices
{
    public class ShiftService : IShiftService
    {
        //Remove after Testing 
        static List<Shift> shifts = new List<Shift>
        {
            new Shift {Id = 1, Name = "Bardienst", Description = "Helfen in der Bar", AgeRestriction = 18, Points = 10},
            new Shift {Id = 2, Name = "Garderobe", Description = "Jacken aufhängen", AgeRestriction = 0, Points = 10},
            new Shift {Id = 3, Name = "Aufräumen", Description = "Nach Veranstaltungsende aufräumen", AgeRestriction = 0, Points = 20}
        };
        public ShiftService() { }
        public Task<Shift> AddShiftAsync(Shift shift)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteShiftAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Shift>> GetAllShiftsAsync()
        {
            return await Task.FromResult(shifts);
        }

        public Task<Shift> GetShiftByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Shift> UpdateShiftAsync(int id, Shift shift)
        {
            throw new NotImplementedException();
        }
    }
}
