using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTOs;
using Services;

namespace ContactManagerTester;

public class CountriesServiceTest
{
    private readonly ICountriesService _countriesService;
    //constructor
    public CountriesServiceTest()
    {
        _countriesService = new CountriesService(new ContactMangerDbContext(new DbContextOptionsBuilder<ContactMangerDbContext>().Options));
    }

    #region AddCountry
    // When CountryAddRequest is null, it should throw ArgumentNullException
    [Fact]
    public void AddCountry_NullCountry()
    {
        //Arrange
        CountryAddRequest? request = null;
        //Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            //Act
            _countriesService.AddCountry(request);
        });
    }
    // When CountryName is null, it should throw ArgumentException
    [Fact]
    public void AddCountry_CountryNameIsNull()
    {
        //Arrange
        CountryAddRequest? request = new CountryAddRequest()
        {
            CountryName = null
        };
        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _countriesService.AddCountry(request);
        });
    }
    // When CountryName is Duplicate, it should throw ArgumentException
    [Fact]
    public void AddCountry_DuplicateCountryName()
    {
        //Arrange
        CountryAddRequest? request1 = new CountryAddRequest()
        {
            CountryName = "USA"
        };
        CountryAddRequest? request2 = new CountryAddRequest()
        {
            CountryName = "USA"
        };
        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _countriesService.AddCountry(request1);
            _countriesService.AddCountry(request2);
        });
    }
    // When the proper CountryName is supplied, it should insent the country to the existing list of countries
    [Fact]
    public void ProperCountryName()
    {
        //Arrange
        CountryAddRequest request = new CountryAddRequest()
        {
            CountryName = "Japan"
        };
        //Act
        CountryResponse response = _countriesService.AddCountry(request);
        //Assert
        List<CountryResponse> countryList = _countriesService.GetAllCountries();
        Assert.True(response.CountryId != Guid.Empty);
        Assert.Contains(response, countryList);
    }
    #endregion

    #region GetAllCountries
    // The list fo countries should be empty by default (before adding any countries)
    [Fact]
    public void GetAllCountries_EmptyList()
    {
        //Acts
        List<CountryResponse> actualCountryResponse = _countriesService.GetAllCountries();
        //Asserts
        Assert.Empty(actualCountryResponse);
    }
    // The list of countries should return all the country
    [Fact]
    public void GetAllCountries_AddFewCountries()
    {
        //Arrange
        List<CountryAddRequest> countryAddRequests = new List<CountryAddRequest>()
        {
            new CountryAddRequest(){CountryName = "USA"},
            new CountryAddRequest(){CountryName = "UK"}
        };
        //Acts
        List<CountryResponse> addedCountries = new List<CountryResponse>();
        foreach (CountryAddRequest countryRequest in countryAddRequests)
        {
            addedCountries.Add(_countriesService.AddCountry(countryRequest));
        }
        //Asserts
        List<CountryResponse> actualCountries = _countriesService.GetAllCountries();
        foreach (CountryResponse input in addedCountries)
        {
            Assert.Contains(input, actualCountries);
        }
    }
    #endregion

    #region GetCountryByCountryId
    //for null countryId, it should return a null response
    [Fact]
    public void GetCountryByCountryId_NullCountryId()
    {
        //Arrange
        Guid? countryId = null;
        //Act
        CountryResponse? response = _countriesService.GetCountryByCountryId(countryId);
        //Assert
        Assert.Null(response);
    }
    //for valid countryId, it should return a valid response
    [Fact]
    public void GetCountryByCountryId_ValidCountryId()
    {
        //Arrange
        CountryAddRequest countryAddRequest = new CountryAddRequest()
        {
            CountryName = "Demo"
        };
        CountryResponse input = _countriesService.AddCountry(countryAddRequest);
        //Act
        CountryResponse? output = _countriesService.GetCountryByCountryId(input.CountryId);
        //Assert
        Assert.Equal(input, output);
    }
    #endregion
}