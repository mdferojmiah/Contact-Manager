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
}