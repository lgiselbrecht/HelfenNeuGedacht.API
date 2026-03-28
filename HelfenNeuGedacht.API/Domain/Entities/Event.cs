using HelfenNeuGedacht.API.Domain.Entities;

public class Event : IEntity
{
    public int Id { get; set; }
    public string? Title {get; set;}
    public string? Description {get; set;}
    public string? Location {get; set; }
    public DateTime StartDate { get; set; } 
    public DateTime EndDate { get; set; }
    public int OrganizationId { get; set; }
    public Organization? Organization { get; set; }
    public ICollection<Shift>? Shift { get; set; }
}