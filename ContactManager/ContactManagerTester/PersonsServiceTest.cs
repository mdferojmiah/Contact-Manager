
using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTOs;
using ServiceContracts.Enums;
using Services;
using Xunit.Abstractions;

namespace ContactManagerTester;

public class PersonsServiceTest
{
    private readonly ICountriesService _countriesService;
    private readonly IPersonsService _personsService; 
    private readonly ITestOutputHelper _testOutputHelper;
    public PersonsServiceTest(ITestOutputHelper testOutputHelper)
    {
        _countriesService = new CountriesService(new ContactMangerDbContext(new DbContextOptionsBuilder<ContactMangerDbContext>().Options));
        _personsService = new PersonsService(new  ContactMangerDbContext(new DbContextOptionsBuilder<ContactMangerDbContext>().Options), _countriesService);
        _testOutputHelper = testOutputHelper;
    }
    #region AddPerson
    //when a null personAddRequest is supplied, it should throw ArgumentNullException
    [Fact]
    public void AddPerson_NullPerson()
    {
        //Arrange
        PersonAddRequest? personAddRequest = null;
        //Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            //Act
            _personsService.AddPerson(personAddRequest);
        });
    }
    //when a null person name in personAddRequest is supplied, it should throw ArgumentException
    [Fact]
    public void AddPerson_NullPersonName()
    {
        //Arrange
        PersonAddRequest personAddRequest = new PersonAddRequest()
        {
            PersonName = null
        };
        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _personsService.AddPerson(personAddRequest);
        });
    }
    //when a null email in personAddRequest is supplied, it should throw ArgumentException
    [Fact]
    public void AddPerson_NullPersonEmail()
    {
        //Arrange
        PersonAddRequest personAddRequest = new PersonAddRequest()
        {
            Email = null
        };
        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _personsService.AddPerson(personAddRequest);
        });
    }
    //when a valid personAddRequest is supplied, it should insert the person into the person list and return an object of PersonResponse which includes the newly generated person id
    [Fact]
    public void AddPerson_ProperPersonDetails()
    {
        //Arrange
        PersonAddRequest personAddRequest = new PersonAddRequest()
        {
            PersonName = "Test Person",
            Email = "test@test.com",
            DateOfBirth = DateTime.Parse("2001-12-05"),
            Gender = GenderOptions.Male,
            CountryId = Guid.NewGuid(),
            Address = "sample address",
            ReceiveNewsLetters = true
        };
        //Act
        PersonResponse personResponse = _personsService.AddPerson(personAddRequest);
        List<PersonResponse> personList = _personsService.GetAllPersons();
        //Assert
        Assert.True(personResponse.PersonId != Guid.Empty);
        Assert.Contains(personResponse, personList);
    }
    #endregion

    #region GetPersonByPersonId
    //if personId is null, then it should return null personResponse
    [Fact]
    public void GetPersonByPersonId_NullPersonId()
    {
        //Arrange
        Guid? personId = null;
        //Act
        PersonResponse? personResponse = _personsService.GetPersonByPersonId(personId);
        //Assert
        Assert.Null(personResponse);
    }
    //if a personId is valid, then it should return valid personResponse
    [Fact]
    public void GetPersonByPersonId_ValidPersonId()
    {
        //Arrange
        CountryAddRequest countryAddRequest = new CountryAddRequest()
        {
            CountryName = "Bangladesh"
        };
        CountryResponse countryResponse = _countriesService.AddCountry(countryAddRequest);
        PersonAddRequest personAddRequest = new PersonAddRequest()
        {
            PersonName = "Test Person",
            Email = "test@test.com",
            DateOfBirth = DateTime.Parse("2001-12-05"),
            Gender = GenderOptions.Male,
            CountryId = countryResponse.CountryId,
            Address = "sample address",
            ReceiveNewsLetters = true
        };
        PersonResponse personResponseInput = _personsService.AddPerson(personAddRequest);
        //Act
        PersonResponse? personResponseOutput = _personsService.GetPersonByPersonId(personResponseInput.PersonId);
        //Assert
        Assert.Equal(personResponseInput, personResponseOutput);
    }
    #endregion

    #region GetAllPerson
    //if the list is empty, it should return an empty list
    [Fact]
    public void GetAllPerson_EmptyList()
    {
        //Act
        List<PersonResponse> persons = _personsService.GetAllPersons();
        //Assert
        Assert.Empty(persons);
    }
    //if we add 2-3 person in the list, it should return all the person information
    [Fact]
    public void GetAllPerson_SomePerson()
    {
        //Arrange
        CountryAddRequest countryAddRequest1 = new CountryAddRequest()
        {
            CountryName = "Bangladesh"
        };
        CountryAddRequest countryAddRequest2 = new CountryAddRequest()
        {
            CountryName = "Iran"
        };
        CountryResponse countryResponse1 = _countriesService.AddCountry(countryAddRequest1);
        CountryResponse countryResponse2 = _countriesService.AddCountry(countryAddRequest2);
        
        PersonAddRequest personAddRequest1 = new PersonAddRequest()
        {
            PersonName = "Feroj Miah",
            Email = "feroj@email.com",
            DateOfBirth = DateTime.Parse("2001-09-11"),
            Gender = GenderOptions.Male,
            CountryId = countryResponse1.CountryId,
            Address = "Address of Feroj",
            ReceiveNewsLetters = true
        };
        PersonAddRequest personAddRequest2 = new PersonAddRequest()
        {
            PersonName = "Nadira Sathi",
            Email = "sath@email.com",
            DateOfBirth = DateTime.Parse("2005-07-01"),
            Gender = GenderOptions.Female,
            CountryId = countryResponse2.CountryId,
            Address = "Address of sathi",
            ReceiveNewsLetters = false
        };
        PersonAddRequest personAddRequest3 = new PersonAddRequest()
        {
            PersonName = "Atiqur Rahman",
            Email = "atike@email.com",
            DateOfBirth = DateTime.Parse("2000-12-05"),
            Gender = GenderOptions.Male,
            CountryId = countryResponse1.CountryId,
            Address = "Address of Atike",
            ReceiveNewsLetters = false
        };
        List<PersonAddRequest> personAddRequests = new List<PersonAddRequest>()
        {
            personAddRequest1, personAddRequest2, personAddRequest3
        };
        
        List<PersonResponse> personResponsesInput = new List<PersonResponse>();
        foreach (PersonAddRequest personAddRequest in personAddRequests)
        {
            PersonResponse personResponse = _personsService.AddPerson(personAddRequest);
            personResponsesInput.Add(personResponse);
        }
        //printing the inputted data
        _testOutputHelper.WriteLine("Expected:");
        foreach (PersonResponse personResponse in personResponsesInput)
        {
            _testOutputHelper.WriteLine(personResponse.ToString());
        }
        
        //Act
        List<PersonResponse> personResponsesOutput = _personsService.GetAllPersons();
        //printing the output data
        _testOutputHelper.WriteLine("Actual:");
        foreach (PersonResponse personResponse in personResponsesOutput)
        {
            _testOutputHelper.WriteLine(personResponse.ToString());
        }
        
        //Assert
        foreach (PersonResponse personResponse in personResponsesInput)
        {
            Assert.Contains(personResponse, personResponsesOutput);
        }
    }
    #endregion

    #region GetFilteredPersons
    //if we search by an empty string, it should return all the person information
    [Fact]
    public void GetFilteredPersons_EmptySearchString()
    {
        //Arrange
        CountryAddRequest countryAddRequest1 = new CountryAddRequest()
        {
            CountryName = "Bangladesh"
        };
        CountryAddRequest countryAddRequest2 = new CountryAddRequest()
        {
            CountryName = "Iran"
        };
        CountryResponse countryResponse1 = _countriesService.AddCountry(countryAddRequest1);
        CountryResponse countryResponse2 = _countriesService.AddCountry(countryAddRequest2);
        
        PersonAddRequest personAddRequest1 = new PersonAddRequest()
        {
            PersonName = "Feroj Miah",
            Email = "feroj@email.com",
            DateOfBirth = DateTime.Parse("2001-09-11"),
            Gender = GenderOptions.Male,
            CountryId = countryResponse1.CountryId,
            Address = "Address of Feroj",
            ReceiveNewsLetters = true
        };
        PersonAddRequest personAddRequest2 = new PersonAddRequest()
        {
            PersonName = "Nadira Sathi",
            Email = "sath@email.com",
            DateOfBirth = DateTime.Parse("2005-07-01"),
            Gender = GenderOptions.Female,
            CountryId = countryResponse2.CountryId,
            Address = "Address of sathi",
            ReceiveNewsLetters = false
        };
        PersonAddRequest personAddRequest3 = new PersonAddRequest()
        {
            PersonName = "Atiqur Rahman",
            Email = "atike@email.com",
            DateOfBirth = DateTime.Parse("2000-12-05"),
            Gender = GenderOptions.Male,
            CountryId = countryResponse1.CountryId,
            Address = "Address of Atike",
            ReceiveNewsLetters = false
        };
        List<PersonAddRequest> personAddRequests = new List<PersonAddRequest>()
        {
            personAddRequest1, personAddRequest2, personAddRequest3
        };
        
        List<PersonResponse> personResponsesInput = new List<PersonResponse>();
        foreach (PersonAddRequest personAddRequest in personAddRequests)
        {
            PersonResponse personResponse = _personsService.AddPerson(personAddRequest);
            personResponsesInput.Add(personResponse);
        }
        //printing the inputted data
        _testOutputHelper.WriteLine("###########Expected:");
        foreach (PersonResponse personResponse in personResponsesInput)
        {
            _testOutputHelper.WriteLine(personResponse.ToString());
        }
        
        //Act
        List<PersonResponse> personResponsesOutput = _personsService.GetFilteredPersons(nameof(Person.PersonName), "");
        //printing the output data
        _testOutputHelper.WriteLine("###########Actual:");
        foreach (PersonResponse personResponse in personResponsesOutput)
        {
            _testOutputHelper.WriteLine(personResponse.ToString());
        }
        
        //Assert
        foreach (PersonResponse personResponse in personResponsesInput)
        {
            Assert.Contains(personResponse, personResponsesOutput);
        }
    }
    //if we search with a valid search string, it should return all the matching persons information
    [Fact]
    public void GetFilteredPersons_ValidSearchString()
    {
        //Arrange
        CountryAddRequest countryAddRequest1 = new CountryAddRequest()
        {
            CountryName = "Bangladesh"
        };
        CountryAddRequest countryAddRequest2 = new CountryAddRequest()
        {
            CountryName = "Iran"
        };
        CountryResponse countryResponse1 = _countriesService.AddCountry(countryAddRequest1);
        CountryResponse countryResponse2 = _countriesService.AddCountry(countryAddRequest2);
        
        PersonAddRequest personAddRequest1 = new PersonAddRequest()
        {
            PersonName = "Feroj Miah",
            Email = "feroj@email.com",
            DateOfBirth = DateTime.Parse("2001-09-11"),
            Gender = GenderOptions.Male,
            CountryId = countryResponse1.CountryId,
            Address = "Address of Feroj",
            ReceiveNewsLetters = true
        };
        PersonAddRequest personAddRequest2 = new PersonAddRequest()
        {
            PersonName = "Nadira Sathi",
            Email = "sath@email.com",
            DateOfBirth = DateTime.Parse("2005-07-01"),
            Gender = GenderOptions.Female,
            CountryId = countryResponse2.CountryId,
            Address = "Address of sathi",
            ReceiveNewsLetters = false
        };
        PersonAddRequest personAddRequest3 = new PersonAddRequest()
        {
            PersonName = "Atiqur Rahman",
            Email = "atike@email.com",
            DateOfBirth = DateTime.Parse("2000-12-05"),
            Gender = GenderOptions.Male,
            CountryId = countryResponse1.CountryId,
            Address = "Address of Atike",
            ReceiveNewsLetters = false
        };
        List<PersonAddRequest> personAddRequests = new List<PersonAddRequest>()
        {
            personAddRequest1, personAddRequest2, personAddRequest3
        };
        
        List<PersonResponse> personResponsesInput = new List<PersonResponse>();
        foreach (PersonAddRequest personAddRequest in personAddRequests)
        {
            PersonResponse personResponse = _personsService.AddPerson(personAddRequest);
            personResponsesInput.Add(personResponse);
        }
        //printing the inputted data
        _testOutputHelper.WriteLine("###########Expected:");
        foreach (PersonResponse personResponse in personResponsesInput)
        {
            _testOutputHelper.WriteLine(personResponse.ToString());
        }
        
        //Act
        List<PersonResponse> personResponsesOutput = _personsService.GetFilteredPersons(nameof(Person.PersonName), "ah");
        //printing the output data
        _testOutputHelper.WriteLine("###########Actual:");
        foreach (PersonResponse personResponse in personResponsesOutput)
        {
            _testOutputHelper.WriteLine(personResponse.ToString());
        }
        
        //Assert
        foreach (PersonResponse personResponse in personResponsesInput)
        {
            if (personResponse.PersonName != null)
            {
                if (personResponse.PersonName.Contains("ah", StringComparison.OrdinalIgnoreCase))
                {
                    Assert.Contains(personResponse, personResponsesOutput);
                }
            }
        }
    }
    #endregion

    #region GetSortedPersons
    //if sortOrder is selected as descending order, it will return the list as descending order
    [Fact]
    public void GetSortedPersons()
    {
        //Arrange
        CountryAddRequest countryAddRequest1 = new CountryAddRequest()
        {
            CountryName = "Bangladesh"
        };
        CountryAddRequest countryAddRequest2 = new CountryAddRequest()
        {
            CountryName = "Iran"
        };
        CountryResponse countryResponse1 = _countriesService.AddCountry(countryAddRequest1);
        CountryResponse countryResponse2 = _countriesService.AddCountry(countryAddRequest2);
        
        PersonAddRequest personAddRequest1 = new PersonAddRequest()
        {
            PersonName = "Feroj Miah",
            Email = "feroj@email.com",
            DateOfBirth = DateTime.Parse("2001-09-11"),
            Gender = GenderOptions.Male,
            CountryId = countryResponse1.CountryId,
            Address = "Address of Feroj",
            ReceiveNewsLetters = true
        };
        PersonAddRequest personAddRequest2 = new PersonAddRequest()
        {
            PersonName = "Nadira Sathi",
            Email = "sath@email.com",
            DateOfBirth = DateTime.Parse("2005-07-01"),
            Gender = GenderOptions.Female,
            CountryId = countryResponse2.CountryId,
            Address = "Address of sathi",
            ReceiveNewsLetters = false
        };
        PersonAddRequest personAddRequest3 = new PersonAddRequest()
        {
            PersonName = "Atiqur Rahman",
            Email = "atike@email.com",
            DateOfBirth = DateTime.Parse("2000-12-05"),
            Gender = GenderOptions.Male,
            CountryId = countryResponse1.CountryId,
            Address = "Address of Atike",
            ReceiveNewsLetters = false
        };
        List<PersonAddRequest> personAddRequests = new List<PersonAddRequest>()
        {
            personAddRequest1, personAddRequest2, personAddRequest3
        };
        
        List<PersonResponse> personResponsesInput = new List<PersonResponse>();
        foreach (PersonAddRequest personAddRequest in personAddRequests)
        {
            PersonResponse personResponse = _personsService.AddPerson(personAddRequest);
            personResponsesInput.Add(personResponse);
        }
        //printing the inputted data
        _testOutputHelper.WriteLine("###########Expected:");
        foreach (PersonResponse personResponse in personResponsesInput)
        {
            _testOutputHelper.WriteLine(personResponse.ToString());
        }
        
        //Act
        List<PersonResponse> allPersons = _personsService.GetAllPersons();
        List<PersonResponse> personResponsesOutput = _personsService.GetSortedPersons(allPersons, nameof(Person.PersonName), SortOrderOptions.Desc);
        //printing the output data
        _testOutputHelper.WriteLine("###########Actual:");
        foreach (PersonResponse personResponse in personResponsesOutput)
        {
            _testOutputHelper.WriteLine(personResponse.ToString());
        }
        personResponsesInput = personResponsesInput.OrderByDescending(temp => temp.PersonName).ToList();
        //Assert
        for (int i = 0; i < personResponsesInput.Count; i++)
        {
            Assert.Equal(personResponsesInput[i], personResponsesOutput[i]);
        }
    }
    #endregion

    #region UpdatePerson
    //If PersonUpdateRequest is null, then it should return ArgumentNullException
    [Fact]
    public void UpdatePerson_NullPersonUpdateRequest()
    {
        //Arrange
        PersonUpdateRequest? personUpdateRequest = null;
        //Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            //Act
            _personsService.UpdatePerson(personUpdateRequest);
        });
    }
    //If invalid personId is submitted, then it should return ArgumentException
    [Fact]
    public void UpdatePerson_InvalidPersonId()
    {
        //Arrange
        PersonUpdateRequest? personUpdateRequest = new PersonUpdateRequest()
        {
            PersonId = Guid.NewGuid()
        };
        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _personsService.UpdatePerson(personUpdateRequest);
        });
    }
    //If person name is null, then it should return ArgumentException
    [Fact]
    public void UpdatePerson_NullPersonName()
    {
        //Arrange
        CountryAddRequest countryAddRequest = new CountryAddRequest()
        {
            CountryName = "Bangladesh"
        };
        CountryResponse countryResponse = _countriesService.AddCountry(countryAddRequest);
        PersonAddRequest personAddRequest = new PersonAddRequest()
        {
            PersonName = "Feroj Miah",
            Email = "01ferojmiah@gmail.com",
            DateOfBirth = DateTime.Parse("2001-12-05"),
            Gender = GenderOptions.Male,
            CountryId = countryResponse.CountryId,
            Address = "Address of Feroj",
            ReceiveNewsLetters = true
        };
        PersonResponse personResponse = _personsService.AddPerson(personAddRequest);
        PersonUpdateRequest personUpdateRequest = personResponse.ToPersonUpdateRequest();
        personUpdateRequest.PersonName = null;
        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _personsService.UpdatePerson(personUpdateRequest);
        });
    }
    //If Email is null, then it should return ArgumentException
    [Fact]
    public void UpdatePerson_NullEmail()
    {
        //Arrange
        CountryAddRequest countryAddRequest = new CountryAddRequest()
        {
            CountryName = "Bangladesh"
        };
        CountryResponse countryResponse = _countriesService.AddCountry(countryAddRequest);
        PersonAddRequest personAddRequest = new PersonAddRequest()
        {
            PersonName = "Feroj Miah",
            Email = "01ferojmiah@gmail.com",
            DateOfBirth = DateTime.Parse("2001-12-05"),
            Gender = GenderOptions.Male,
            CountryId = countryResponse.CountryId,
            Address = "Address of Feroj",
            ReceiveNewsLetters = true
        };
        PersonResponse personResponse = _personsService.AddPerson(personAddRequest);
        PersonUpdateRequest personUpdateRequest = personResponse.ToPersonUpdateRequest();
        personUpdateRequest.Email = null;
        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _personsService.UpdatePerson(personUpdateRequest);
        });
    }
    //If every data is valid, then it should return valid PersonResponse
    [Fact]
    public void UpdatePerson_ValidPersonUpdateRequest()
    {
        //Arrange
        CountryAddRequest countryAddRequest = new CountryAddRequest()
        {
            CountryName = "Bangladesh"
        };
        CountryResponse countryResponse = _countriesService.AddCountry(countryAddRequest);
        PersonAddRequest personAddRequest = new PersonAddRequest()
        {
            PersonName = "Feroj Miah",
            Email = "01ferojmiah@gmail.com",
            DateOfBirth = DateTime.Parse("2001-12-05"),
            Gender = GenderOptions.Male,
            CountryId = countryResponse.CountryId,
            Address = "Address of Feroj",
            ReceiveNewsLetters = true
        };
        PersonResponse personResponse = _personsService.AddPerson(personAddRequest);
        PersonUpdateRequest personUpdateRequest = personResponse.ToPersonUpdateRequest();
        //updating details
        personUpdateRequest.PersonName = "Atiqur Rahman";
        personUpdateRequest.Email = "mdatiqur0171@gmail.com";
        personUpdateRequest.Address = "Updated address of Atike";
        //Act
        PersonResponse personResponseOutput = _personsService.UpdatePerson(personUpdateRequest);
        PersonResponse? personResponseInput = _personsService.GetPersonByPersonId(personResponse.PersonId);
        //Assert
        Assert.Equal(personResponseInput,personResponseOutput);
    }
    #endregion

    #region DeletePerson
    // if personId is null, it returns argumentNullException
    [Fact]
    public void DeletePerson_NullPeronId()
    {
        //Arrange
        Guid? personId = null;
        //Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            //Act 
            _personsService.DeletePerson(personId);
        });
    }
    // if personId is invalid, it returns false
    [Fact]
    public void DeletePerson_InvalidPeronId()
    {
        //Act 
        bool isDeleted = _personsService.DeletePerson(Guid.NewGuid());
        //Assert
        Assert.False(isDeleted);
    }
    // if personId is valid, it returns true
    [Fact]
    public void DeletePerson_ValidPeronId()
    {
        //Arrange
        CountryAddRequest countryAddRequest = new CountryAddRequest()
        {
            CountryName = "Bangladesh"
        };
        CountryResponse countryResponse = _countriesService.AddCountry(countryAddRequest);
        PersonAddRequest personAddRequest = new PersonAddRequest()
        {
            PersonName = "Feroj Miah",
            Email = "01ferojmiah@gmail.com",
            DateOfBirth = DateTime.Parse("2001-12-05"),
            Gender = GenderOptions.Male,
            CountryId = countryResponse.CountryId,
            Address = "Address of Feroj",
            ReceiveNewsLetters = true
        };
        PersonResponse personResponse = _personsService.AddPerson(personAddRequest);
        //Act 
        bool isDeleted = _personsService.DeletePerson(personResponse.PersonId);
        //Assert
        Assert.True(isDeleted);
    }
    #endregion
}