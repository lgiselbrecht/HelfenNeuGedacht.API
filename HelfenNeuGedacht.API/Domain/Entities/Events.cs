using HelfenNeuGedacht.API.Domain.Entities;

public class Events : IEntity
{
    public int Id { get; set; }
    public required string Title {get; set;}
    public required string Description {get; set;}
    public required string Location {get; set; }
    public DateTime StartDate { get; set; } 
    public DateTime EndDate { get; set; }
    public int OrganizationId { get; set; }
    public required Organization Organization { get; set; }
    public required ICollection<Shifts> Shift { get; set; }
}