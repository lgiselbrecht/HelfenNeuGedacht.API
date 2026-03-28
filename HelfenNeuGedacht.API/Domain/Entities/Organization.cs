using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HelfenNeuGedacht.API.Domain.Entities;

public class Organization : IEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string RegistrationNumber { get; set; } = string.Empty;
    public string? Type { get; set; } // z.B. Verein, Gemeinde, Feuerwehr

    public string? Website { get; set; }
    
    public string? Street { get; set; }

    public string? PostalCode { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public string ContactEmail { get; set; } = string.Empty;

    public string? ContactPhone { get; set; }

    public string? ContactPersonName { get; set; }

    public string? ContactPersonRole { get; set; } 

    public OrganizationApprovalStatus ApprovalStatus { get; set; } = OrganizationApprovalStatus.Pending;

    public bool IsApproved { get; set; } = false;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public string? ApprovedBy { get; set; } 

    public string? RejectionReason { get; set; }

    public ICollection<Event> Events { get; set; } = new List<Event>();

 }

public enum OrganizationApprovalStatus
{
    Pending = 0,
    Approved = 1,
    Rejected = 2,
    Suspended = 3
}