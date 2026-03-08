using HelfenNeuGedacht.API.Application.Services.ShiftServices.Dto;
using HelfenNeuGedacht.API.Domain.Entities;

namespace HelfenNeuGedacht.API.Application.Mapper
{
    public class DtoMapper
    {
        public ShiftResponse ToShiftResponse(Shift shift)
        {
            return new ShiftResponse()
            {
                Id = shift.Id,
                Name = shift.Name,
                Description = shift.Description,
                Requirements = shift.Requirements,
                AgeRestriction = shift.AgeRestriction,
                Points = shift.Points,
            };
        }
    }
}
