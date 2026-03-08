using System.Diagnostics.Eventing.Reader;
using HelfenNeuGedacht.API.Domain;

namespace HelfenNeuGedacht.API.Application.Services
{
    public interface IShiftService
    {
        Task<List<Shift>> GetAllShiftsAsync();
        Task<Shift> GetShiftByIdAsync(int id);
        Task<Shift> AddShiftAsync(Shift shift);
        Task<Shift> UpdateShiftAsync(int id, Shift shift);
        Task<bool> DeleteShiftAsync(int id);

    }
}
