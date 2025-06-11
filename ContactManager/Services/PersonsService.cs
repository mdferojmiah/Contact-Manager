using System.Runtime.InteropServices.Marshalling;
using Entities;
using ServiceContracts;
using ServiceContracts.DTOs;
using Services.Helpers;

namespace Services;
/// <summary>
/// Implements IPersonsService interface
/// </summary>
public class PersonsService : IPersonsService
{
    private readonly List<Person> _persons = new();
    private readonly ICountriesService _countriesService = new CountriesService();

    private PersonResponse ConvertPersonToPersonResponse(Person person)
    {
        PersonResponse personResponse = person.ToPersonResponse();
        personResponse.Country = _countriesService.GetCountryByCountryId(person.CountryId)?.CountryName;
        return personResponse;
    }
    public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
    {
        //when personAddRequest is null
        if (personAddRequest == null)
        {
            throw new ArgumentNullException(nameof(personAddRequest));
        }
        //model validation
        ValidationHelper.ModelValidation(personAddRequest);
        //Converting PersonAddRequest to Person's object
        Person person = personAddRequest.ToPerson();
        //creating new guid for person
        person.PersonId = Guid.NewGuid();
        //adding to the list
        _persons.Add(person);
        return ConvertPersonToPersonResponse(person);
    }

    public List<PersonResponse> GetAllPersons()
    {
        return _persons.Select(person => person.ToPersonResponse()).ToList();
    }

    public PersonResponse? GetPersonByPersonId(Guid? personId)
    {
        if (personId == null) return null;
        Person? person = _persons.FirstOrDefault(temp => temp.PersonId == personId);
        if (person == null) return null;
        return person.ToPersonResponse();
    }

    public List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString)
    {
        List<PersonResponse> allPersons = GetAllPersons();
        List<PersonResponse> matchingPersons = allPersons;
        if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
            return matchingPersons;
        switch (searchBy)
        {
            case nameof(Person.PersonName):
                matchingPersons = allPersons.Where(temp => 
                    // ReSharper disable once SimplifyConditionalTernaryExpression
                    (!string.IsNullOrEmpty(temp.PersonName)? 
                        temp.PersonName.Contains(searchString, StringComparison.OrdinalIgnoreCase) 
                        : true)).ToList();
                break;
            case nameof(Person.Email):
                matchingPersons = allPersons.Where(temp => 
                    // ReSharper disable once SimplifyConditionalTernaryExpression
                    (!string.IsNullOrEmpty(temp.Email) 
                        ? temp.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase) 
                        : true)).ToList();
                break;
            case nameof(Person.DateOfBirth):
                matchingPersons = allPersons.Where(temp => 
                    // ReSharper disable once SimplifyConditionalTernaryExpression
                    (temp.DateOfBirth != null) 
                        ? temp.DateOfBirth.Value.ToString("yyyy MMMM dd").Contains(searchString, StringComparison.OrdinalIgnoreCase) 
                        : true).ToList();
                break;
            case nameof(Person.Gender):
                matchingPersons = allPersons.Where(temp => 
                    // ReSharper disable once SimplifyConditionalTernaryExpression
                    (!string.IsNullOrEmpty(temp.Gender) 
                        ? temp.Gender.Contains(searchString, StringComparison.OrdinalIgnoreCase) 
                        : true)).ToList();
                break;
            case nameof(Person.CountryId):
                matchingPersons = allPersons.Where(temp => 
                    // ReSharper disable once SimplifyConditionalTernaryExpression
                    (!string.IsNullOrEmpty(temp.Country) 
                        ? temp.Country.Contains(searchString, StringComparison.OrdinalIgnoreCase) 
                        : true)).ToList();
                break;
            case nameof(Person.Address):
                matchingPersons = allPersons.Where(temp => 
                    // ReSharper disable once SimplifyConditionalTernaryExpression
                    (!string.IsNullOrEmpty(temp.Address) 
                        ? temp.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase) 
                        : true)).ToList();
                break;
            default: 
                matchingPersons = allPersons;
                break;
        }
        return matchingPersons;
    }
}