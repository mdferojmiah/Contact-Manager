using Entities;

namespace ServiceContracts.DTOs;

/// <summary>
/// Represents a data transfer object (DTO) for a country response.
/// Contains properties for identifying and describing a country entity.
/// </summary>
public class CountryResponse
{
    public Guid CountryId { get; set; }
    public string? CountryName { get; set; }
    //It compares the current object to another object of CountryResponse type and returns true if both values are same; otherwise returns false
    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }
        if (obj.GetType() != typeof(CountryResponse))
        {
            return false;
        }
        CountryResponse countryResponseObj = (CountryResponse)obj;
        return CountryId == countryResponseObj.CountryId && CountryName == countryResponseObj.CountryName;
    }
    public override int GetHashCode()
    {
        // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
        return base.GetHashCode();
    }
}

public static class CountryExtensions
{
    public static CountryResponse ToCountryResponse(this Country country)
    {
        return new CountryResponse()
        {
            CountryId = country.CountryId,
            CountryName = country.CountryName
        };
    }
}