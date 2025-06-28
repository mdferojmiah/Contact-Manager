using System.ComponentModel.DataAnnotations;

namespace Entities;

/// <summary>
/// Represents a country entity with properties for unique identification and name.
/// </summary>
public class Country
{
    [Key]
    public Guid CountryId { get; set; }
    [StringLength(40)]
    public string? CountryName { get; set; }
}