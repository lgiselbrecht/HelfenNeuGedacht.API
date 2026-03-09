using System.Diagnostics.Eventing.Reader;
using HelfenNeuGedacht.API.Application.Services.ShiftServices.Dto;
using HelfenNeuGedacht.API.Domain.Entities;

namespace HelfenNeuGedacht.API.Application.Services.ShiftServices
{
    public interface IShiftService
    {
        Task<List<ShiftResponse>> GetAllShiftsAsync();
        Task<ShiftResponse> GetShiftByIdAsync(int id);
        Task<ShiftResponse> AddShiftAsync(CreateShiftRequest shift);
        Task<ShiftResponse> UpdateShiftAsync(int id, UpdateShiftRequest shift);
        Task<ShiftResponse> DeleteShiftAsync(int id);

    }
}
