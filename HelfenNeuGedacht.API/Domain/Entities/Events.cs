public class Events
{
    public required string ID { get; set; }
    public required string Title {get; set;}
    public required string Description {get; set;}
    public required string Location {get; set; }
    public DateTime StartDate { get; set; } 
    public DateTime EndDate { get; set; }
    public Guid OrganizationId { get; set; }
    public required Organizations Organization { get; set; }
    public required ICollection<Shifts> Shift { get; set; }
}