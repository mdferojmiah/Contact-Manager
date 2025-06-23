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
    private readonly List<Person> _persons;
    private readonly ICountriesService _countriesService;

    public PersonsService(bool initialize = true)
    {
        _persons = new List<Person>();
        _countriesService = new CountriesService();
        if (initialize)
        {
            _persons.Add(new Person()
            {
                PersonId = Guid.Parse("6b93c1dc-3469-4951-a498-3f732f3937ee"),
                PersonName = "Royall Tattersfield",
                Email = "rtattersfield0@salon.com",
                DateOfBirth = DateTime.Parse("2000-02-14"),
                Gender = "Male",
                Address = "2012 7th Parkway",
                ReceiveNewsLetters = true,
                CountryId = Guid.Parse("c3145111-6ce9-4bcd-a75c-32f5ae8f9b05") // UAE
            });
            _persons.Add(new Person()
            {
                PersonId = Guid.Parse("bdfe6826-4781-4281-8b44-c3f3fc2703b1"),
                PersonName = "Jabez Oleksinski",
                Email = "joleksinski1@buzzfeed.com",
                DateOfBirth = DateTime.Parse("1997-09-08"),
                Gender = "Male",
                Address = "99478 Darwin Terrace",
                ReceiveNewsLetters = false,
                CountryId = Guid.Parse("aff89808-67a7-443b-b6b9-3df92996c56b") // Bangladesh
            });
            _persons.Add(new Person()
            {
                PersonId = Guid.Parse("c597de98-6a97-4554-aae5-ec2a6f69627b"),
                PersonName = "Aurie Sheehan",
                Email = "asheehan2@icio.us",
                DateOfBirth = DateTime.Parse("1994-12-08"),
                Gender = "Female",
                Address = "8 Arapahoe Parkway",
                ReceiveNewsLetters = false,
                CountryId = Guid.Parse("a68d7aae-cdfa-4d95-a837-e44c7b5e48f2") // Iran
            });
            _persons.Add(new Person()
            {
                PersonId = Guid.Parse("756aea66-396c-4584-bd44-1e8341394025"),
                PersonName = "Con McClounan",
                Email = "cmcclounan3@diigo.com",
                DateOfBirth = DateTime.Parse("2001-08-29"),
                Gender = "Male",
                Address = "385 Pleasure Pass",
                ReceiveNewsLetters = false,
                CountryId = Guid.Parse("f9d729fb-ed49-4026-84b8-4e950002dfc4") // Pakistan
            });
            _persons.Add(new Person()
            {
                PersonId = Guid.Parse("b3286416-e6b5-42e1-9155-e89bdfa5eed1"),
                PersonName = "Celinka Lipgens",
                Email = "clipgens4@phpbb.com",
                DateOfBirth = DateTime.Parse("2003-12-22"),
                Gender = "Female",
                Address = "43 Cherokee Avenue",
                ReceiveNewsLetters = false,
                CountryId = Guid.Parse("a9739606-55d8-466e-9db1-c0b513547b7d") // Palestine
            });
            _persons.Add(new Person()
            {
                PersonId = Guid.Parse("7f2d236f-8136-4c0c-8f24-b677a6155d3b"),
                PersonName = "Benyamin Dumberrill",
                Email = "bdumberrill5@networksolutions.com",
                DateOfBirth = DateTime.Parse("2000-11-15"),
                Gender = "Male",
                Address = "382 Aberg Place",
                ReceiveNewsLetters = true,
                CountryId = Guid.Parse("c3145111-6ce9-4bcd-a75c-32f5ae8f9b05") // UAE (repeated)
            });
            _persons.Add(new Person()
            {
                PersonId = Guid.Parse("57681644-9413-4498-bdf5-6a5baabedfcf"),
                PersonName = "Beltran Chiles",
                Email = "bchiles6@godaddy.com",
                DateOfBirth = DateTime.Parse("1994-02-02"),
                Gender = "Male",
                Address = "792 Armistice Park",
                ReceiveNewsLetters = false,
                CountryId = Guid.Parse("aff89808-67a7-443b-b6b9-3df92996c56b") // Bangladesh (repeated)
            });
            _persons.Add(new Person()
            {
                PersonId = Guid.Parse("8122f986-f65d-4f6c-964f-84143138a362"),
                PersonName = "Benny Orrom",
                Email = "borrom7@furl.net",
                DateOfBirth = DateTime.Parse("1998-08-09"),
                Gender = "Male",
                Address = "21 Saint Paul Hill",
                ReceiveNewsLetters = true,
                CountryId = Guid.Parse("a68d7aae-cdfa-4d95-a837-e44c7b5e48f2") // Iran (repeated)
            });
            _persons.Add(new Person()
            {
                PersonId = Guid.Parse("ff790c35-2062-41e0-baa0-399d1d3edffc"),
                PersonName = "Alexia Ilyasov",
                Email = "ailyasov8@chicagotribune.com",
                DateOfBirth = DateTime.Parse("1992-08-03"),
                Gender = "Female",
                Address = "587 Bay Crossing",
                ReceiveNewsLetters = true,
                CountryId = Guid.Parse("f9d729fb-ed49-4026-84b8-4e950002dfc4") // Pakistan (repeated)
            });
            _persons.Add(new Person()
            {
                PersonId = Guid.Parse("673a905d-410b-481d-9d6b-c751033047d9"),
                PersonName = "Florry Pickaver",
                Email = "fpickaver9@aol.com",
                DateOfBirth = DateTime.Parse("1991-09-26"),
                Gender = "Female",
                Address = "6726 Transport Way",
                ReceiveNewsLetters = true,
                CountryId = Guid.Parse("a9739606-55d8-466e-9db1-c0b513547b7d") // Palestine (repeated)
            });
        }
    }

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
        return _persons.Select(person => ConvertPersonToPersonResponse(person)).ToList();
    }

    public PersonResponse? GetPersonByPersonId(Guid? personId)
    {
        if (personId == null) return null;
        Person? person = _persons.FirstOrDefault(temp => temp.PersonId == personId);
        if (person == null) return null;
        return ConvertPersonToPersonResponse(person);
    }

    public List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString)
    {
        List<PersonResponse> allPersons = GetAllPersons();
        List<PersonResponse> matchingPersons = allPersons;
        if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
            return matchingPersons;
        switch (searchBy)
        {
            case nameof(PersonResponse.PersonName):
                matchingPersons = allPersons.Where(temp =>
                    // ReSharper disable once SimplifyConditionalTernaryExpression
                    (!string.IsNullOrEmpty(temp.PersonName)
                        ? temp.PersonName.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                        : true)).ToList();
                break;
            case nameof(PersonResponse.Email):
                matchingPersons = allPersons.Where(temp =>
                    // ReSharper disable once SimplifyConditionalTernaryExpression
                    (!string.IsNullOrEmpty(temp.Email)
                        ? temp.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                        : true)).ToList();
                break;
            case nameof(PersonResponse.DateOfBirth):
                matchingPersons = allPersons.Where(temp =>
                    // ReSharper disable once SimplifyConditionalTernaryExpression
                    (temp.DateOfBirth != null)
                        ? temp.DateOfBirth.Value.ToString("yyyy MMMM dd")
                            .Contains(searchString, StringComparison.OrdinalIgnoreCase)
                        : true).ToList();
                break;
            case nameof(PersonResponse.Gender):
                matchingPersons = allPersons.Where(temp =>
                    // ReSharper disable once SimplifyConditionalTernaryExpression
                    (!string.IsNullOrEmpty(temp.Gender)
                        ? temp.Gender.Equals(searchString, StringComparison.OrdinalIgnoreCase)
                        : true)).ToList();
                break;
            case nameof(PersonResponse.CountryId):
                matchingPersons = allPersons.Where(temp =>
                    // ReSharper disable once SimplifyConditionalTernaryExpression
                    (!string.IsNullOrEmpty(temp.Country)
                        ? temp.Country.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                        : true)).ToList();
                break;
            case nameof(PersonResponse.Address):
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

    public List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy,
        SortOrderOptions sortOrder)
    {
        if (string.IsNullOrEmpty(sortBy))
        {
            return allPersons;
        }

        List<PersonResponse> sortedPersons = (sortBy, sortOrder) switch
        {
            //order by PersonName
            (nameof(PersonResponse.PersonName), SortOrderOptions.Asc) => allPersons
                .OrderBy(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.PersonName), SortOrderOptions.Desc) => allPersons
                .OrderByDescending(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),
            //order by Email
            (nameof(PersonResponse.Email), SortOrderOptions.Asc) => allPersons
                .OrderBy(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.Email), SortOrderOptions.Desc) => allPersons
                .OrderByDescending(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),
            //order by DateOfBirth
            (nameof(PersonResponse.DateOfBirth), SortOrderOptions.Asc) => allPersons.OrderBy(temp => temp.DateOfBirth)
                .ToList(),
            (nameof(PersonResponse.DateOfBirth), SortOrderOptions.Desc) => allPersons
                .OrderByDescending(temp => temp.DateOfBirth).ToList(),
            //order by Gender
            (nameof(PersonResponse.Gender), SortOrderOptions.Asc) => allPersons
                .OrderBy(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.Gender), SortOrderOptions.Desc) => allPersons
                .OrderByDescending(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),
            //order by Address
            (nameof(PersonResponse.Address), SortOrderOptions.Asc) => allPersons
                .OrderBy(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.Address), SortOrderOptions.Desc) => allPersons
                .OrderByDescending(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),
            //order by Age
            (nameof(PersonResponse.Age), SortOrderOptions.Asc) => allPersons.OrderBy(temp => temp.Age).ToList(),
            (nameof(PersonResponse.Age), SortOrderOptions.Desc) => allPersons.OrderByDescending(temp => temp.Age)
                .ToList(),
            //order by Country
            (nameof(PersonResponse.Country), SortOrderOptions.Asc) => allPersons
                .OrderBy(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.Country), SortOrderOptions.Desc) => allPersons
                .OrderByDescending(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),
            //order by ReceivedNewsLetters
            (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.Asc) => allPersons
                .OrderBy(temp => temp.ReceiveNewsLetters).ToList(),
            (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.Desc) => allPersons
                .OrderByDescending(temp => temp.ReceiveNewsLetters).ToList(),
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
        return ConvertPersonToPersonResponse(matchingPerson);
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