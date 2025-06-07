namespace Entities;

/// <summary>
/// Represents a country entity with properties for unique identification and name.
/// </summary>
public class Country
{
    public Guid CountryId { get; set; }
    public string? CountryName { get; set; }
}