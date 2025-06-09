using Entities;
using ServiceContracts;
using ServiceContracts.DTOs;

namespace Services;
/// <summary>
/// Implements all the country related methods
/// </summary>
public class CountriesService: ICountriesService
{
    private readonly List<Country> _countries = new();

    public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
    {
        // validation: countryAddRequest can't be null
        if (countryAddRequest == null)
        {
            throw new ArgumentNullException(nameof(countryAddRequest));
        }
        //validation: countryName can't be null
        if (countryAddRequest.CountryName == null)
        {
            throw new ArgumentException(nameof(countryAddRequest.CountryName));
        }
        //validation: countryName can't be duplicate
        if (_countries.Any(temp => temp.CountryName == countryAddRequest.CountryName))
        {
            throw new ArgumentException("Duplicate CountryName Entry!");
        }
        //converting countryAddRequest to Country domain
        Country country = countryAddRequest.ToCountry();
        //create new Guid for countryId
        country.CountryId = Guid.NewGuid();
        //add country into the list
        _countries.Add(country);
        //converting the country into countryResponse and returning it
        return country.ToCountryResponse();
    }

    public List<CountryResponse> GetAllCountries()
    {
        //select all the country from the countryList
        //convert it into countryResponse type 
        //return it
        return _countries.Select(country => country.ToCountryResponse()).ToList();
    }

    public CountryResponse? GetCountryByCountryId(Guid? countryId)
    {
        //if countryId is null, it should return null
        if (countryId == null) return null;
        //searching country from the countryList
        Country? country = _countries.FirstOrDefault(temp => temp.CountryId == countryId);
        //if the given response is null, it should return null
        if (country == null) return null;
        //return the country object of CountryResponse type
        return country.ToCountryResponse();
    }
}