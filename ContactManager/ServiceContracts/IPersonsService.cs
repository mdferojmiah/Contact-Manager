using ServiceContracts.DTOs;

namespace ServiceContracts;
/// <summary>
/// Service interface for all the peron service method
/// </summary>
public interface IPersonsService
{
    /// <summary>
    /// Adds a person to a list
    /// </summary>
    /// <param name="personAddRequest">DTO to add person request</param>
    /// <returns>PeronResponse time object with peron information</returns>
    PersonResponse AddPerson(PersonAddRequest? personAddRequest);
    /// <summary>
    /// Returns all the persons form the list
    /// </summary>
    /// <returns>Returns a list object of PersonResponse</returns>
    List<PersonResponse> GetAllPersons();
    /// <summary>
    ///  Matches PersonId and return the Person object
    /// </summary>
    /// <param name="personId">PersonId to search</param>
    /// <returns>Person object as PersonResponse</returns>
    PersonResponse? GetPersonByPersonId(Guid? personId);
    /// <summary>
    /// returns all the person details based on the searchBy and searchString
    /// </summary>
    /// <param name="searchBy">PersonName, Email or something else</param>
    /// <param name="searchString">The actual string you want to search</param>
    /// <returns>list of the persons matched with the searched data</returns>
    List<PersonResponse> GetFilteredPersons(String searchBy, String? searchString);
}