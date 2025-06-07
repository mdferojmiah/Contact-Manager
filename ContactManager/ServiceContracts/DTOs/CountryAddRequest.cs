using Entities;

namespace ServiceContracts.DTOs;

/// <summary>
/// Represents the request object for adding a new country.
/// </summary>
public class CountryAddRequest
{
    public string? CountryName { get; set; }

    public Country ToCountry()
    {
        return new Country()
        {
            CountryName = CountryName
        };
    }
}

