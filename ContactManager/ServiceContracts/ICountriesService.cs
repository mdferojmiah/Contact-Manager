using ServiceContracts.DTOs;

namespace ServiceContracts;

/// <summary>
/// Defines the service contract for managing countries.
/// This service provides functionality for adding new countries
/// and performing related operations involving country data.
/// </summary>
public interface ICountriesService
{
    /// <summary>
    /// Adds a country object to the list of countries
    /// </summary>
    /// <param name="countryAddRequest">Country object to be added</param>
    /// <returns>Returns country objects as countryResponse</returns>
    CountryResponse AddCountry(CountryAddRequest? countryAddRequest);
/// <summary>
/// returns all the countries from the country list
/// </summary>
/// <returns>All countries form the list as List of CountryResponse</returns>
    List<CountryResponse> GetAllCountries();
/// <summary>
/// returns the country according to the given countryId
/// </summary>
/// <param name="countryId">a valid countryId(guid)</param>
/// <returns>a country information CountryResponse type</returns>
    CountryResponse? GetCountryByCountryId(Guid? countryId);
}