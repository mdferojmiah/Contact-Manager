using Entities;
using ServiceContracts;
using ServiceContracts.DTOs;

namespace Services;
/// <summary>
/// Implements all the country related methods
/// </summary>
public class CountriesService: ICountriesService
{
    private readonly List<Country> _countries;
    public CountriesService(bool initialize = true)
    {
        _countries = new List<Country>();
        if (initialize)
        {
            // mock data for countries
            _countries.AddRange(new List<Country>()
            {
                new Country()
                {
                    CountryId = Guid.Parse("c3145111-6ce9-4bcd-a75c-32f5ae8f9b05"),
                    CountryName = "UAE"
                },
                new Country() 
                {
                    CountryId = Guid.Parse("aff89808-67a7-443b-b6b9-3df92996c56b"),
                    CountryName = "Bangladesh"
                },
                new Country() 
                {
                    CountryId = Guid.Parse("a68d7aae-cdfa-4d95-a837-e44c7b5e48f2"),
                    CountryName = "Iran"
                },
                new Country()
                {
                    CountryId = Guid.Parse("f9d729fb-ed49-4026-84b8-4e950002dfc4"),
                    CountryName = "Pakistan"
                },
                new Country()
                {
                    CountryId = Guid.Parse("a9739606-55d8-466e-9db1-c0b513547b7d"),
                    CountryName = "Palestine"
                }
            });
        }
    }

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