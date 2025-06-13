using System.ComponentModel.DataAnnotations;
using Entities;
using ServiceContracts.Enums;

namespace ServiceContracts.DTOs;
/// <summary>
/// DTO to receive the updated data from the UI
/// </summary>
public class PersonUpdateRequest
{
    [Required(ErrorMessage = "PeronId can't be empty!")]
    public Guid PersonId { get; set; }
    [Required(ErrorMessage = "Person Name can't be empty")]
    public string? PersonName { get; set; }
    [Required(ErrorMessage = "Email can't be empty")]
    [EmailAddress(ErrorMessage = "Enter a valid Email")]
    public string? Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public GenderOptions? Gender { get; set; }
    public Guid? CountryId { get; set; }
    public string? Address { get; set; }
    public bool ReceiveNewsLetters { get; set; }
    /// <summary>
    /// Converts PersonUpdateRequest to a Person type object
    /// </summary>
    /// <returns>Person object of a Person type</returns>
    public Person ToPerson()
    {
        return new Person()
        {
            PersonId = PersonId,
            PersonName = PersonName,
            Email = Email,
            DateOfBirth = DateOfBirth,
            Gender = Gender.ToString(),
            CountryId = CountryId,
            Address = Address,
            ReceiveNewsLetters = ReceiveNewsLetters
        };
    }
}