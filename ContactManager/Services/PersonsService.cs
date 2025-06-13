
using Entities;
using ServiceContracts;
using ServiceContracts.DTOs;
using ServiceContracts.Enums;
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

    public List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder)
    {
        if (string.IsNullOrEmpty(sortBy))
        {
            return allPersons;
        }
        List<PersonResponse> sortedPersons = (sortBy, sortOrder) switch
        {
            //order by PersonName
            (nameof(PersonResponse.PersonName),SortOrderOptions.Asc) => allPersons.OrderBy(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.PersonName),SortOrderOptions.Desc) => allPersons.OrderByDescending(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),
            //order by Email
            (nameof(PersonResponse.Email),SortOrderOptions.Asc) => allPersons.OrderBy(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.Email),SortOrderOptions.Desc) => allPersons.OrderByDescending(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),
            //order by DateOfBirth
            (nameof(PersonResponse.DateOfBirth),SortOrderOptions.Asc) => allPersons.OrderBy(temp => temp.DateOfBirth).ToList(),
            (nameof(PersonResponse.DateOfBirth),SortOrderOptions.Desc) => allPersons.OrderByDescending(temp => temp.DateOfBirth).ToList(),
            //order by Gender
            (nameof(PersonResponse.Gender),SortOrderOptions.Asc) => allPersons.OrderBy(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.Gender),SortOrderOptions.Desc) => allPersons.OrderByDescending(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),
            //order by Address
            (nameof(PersonResponse.Address),SortOrderOptions.Asc) => allPersons.OrderBy(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.Address),SortOrderOptions.Desc) => allPersons.OrderByDescending(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),
            //order by Age
            (nameof(PersonResponse.Age),SortOrderOptions.Asc) => allPersons.OrderBy(temp => temp.Age).ToList(),
            (nameof(PersonResponse.Age),SortOrderOptions.Desc) => allPersons.OrderByDescending(temp => temp.Age).ToList(),
            //order by Country
            (nameof(PersonResponse.Country),SortOrderOptions.Asc) => allPersons.OrderBy(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.Country),SortOrderOptions.Desc) => allPersons.OrderByDescending(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),
            //order by ReceivedNewsLetters
            (nameof(PersonResponse.ReceiveNewsLetters),SortOrderOptions.Asc) => allPersons.OrderBy(temp => temp.ReceiveNewsLetters).ToList(),
            (nameof(PersonResponse.ReceiveNewsLetters),SortOrderOptions.Desc) => allPersons.OrderByDescending(temp => temp.ReceiveNewsLetters).ToList(),
            //default
            _ => allPersons
        };
        return sortedPersons;
    }

    public PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest)
    {
        if (personUpdateRequest == null)
        {
            throw new ArgumentNullException(nameof(Person));
        }
        //Validation
        ValidationHelper.ModelValidation(personUpdateRequest);
        //get matching persons object
        Person? matchingPerson = _persons.FirstOrDefault(temp => temp.PersonId == personUpdateRequest.PersonId);
        if (matchingPerson == null)
        {
            throw new ArgumentException("Given Person Id doesn't exists!");
        }
        //updating the data
        matchingPerson.PersonName = personUpdateRequest.PersonName;
        matchingPerson.Email = personUpdateRequest.Email;
        matchingPerson.DateOfBirth = personUpdateRequest.DateOfBirth;
        matchingPerson.Gender = personUpdateRequest.Gender.ToString();
        matchingPerson.CountryId = personUpdateRequest.CountryId;
        matchingPerson.Address = personUpdateRequest.Address;
        matchingPerson.ReceiveNewsLetters = personUpdateRequest.ReceiveNewsLetters;
        return matchingPerson.ToPersonResponse();
    }

    public bool DeletePerson(Guid? personId)
    {
        if (personId == null)
        {
            throw new ArgumentNullException(nameof(personId));
        }
        Person? matchingPerson = _persons.FirstOrDefault(temp => temp.PersonId == personId);
        if (matchingPerson == null)
        {
            return false;
        }

        _persons.RemoveAll(temp => temp.PersonId == personId);
        return true;
    }
}