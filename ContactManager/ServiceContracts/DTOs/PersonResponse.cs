using Entities;
using ServiceContracts.Enums;

namespace ServiceContracts.DTOs;
/// <summary>
/// 
/// </summary>
public class PersonResponse
{
    public Guid PersonId { get; set; }
    public string? PersonName { get; set; }
    public string? Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public Guid? CountryId { get; set; }
    public string? Country { get; set; }
    public string? Address { get; set; }
    public bool ReceiveNewsLetters { get; set; }
    public int? Age { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if (obj.GetType() != typeof(PersonResponse)) return false;
        PersonResponse person = (PersonResponse)obj;
        return PersonId == person.PersonId 
               && PersonName == person.PersonName
               && Email == person.Email
               && DateOfBirth == person.DateOfBirth
               && Gender == person.Gender
               && CountryId == person.CountryId
               && Address == person.Address
               && ReceiveNewsLetters == person.ReceiveNewsLetters;
    }

    public override int GetHashCode()
    {
        // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return $"Person Id: {PersonId}\n" +
               $"Person Name: {PersonName}\n" +
               $"Email: {Email}\n" +
               $"Gender: {Gender}\n" +
               $"Date of Birth: {DateOfBirth}\n" +
               $"Country: {Country}\n" +
               $"Country Id: {CountryId}\n" +
               $"Address: {Address}\n" +
               $"ReceiveNewsLetters: {ReceiveNewsLetters}\n" +
               $"Age: {Age}\n" +
               $"---------------------\n";
    }
    public PersonUpdateRequest ToPersonUpdateRequest()
    {
        return new PersonUpdateRequest()
        {
            PersonId = PersonId,
            PersonName = PersonName,
            Email = Email,
            DateOfBirth = DateOfBirth,
            Gender = (GenderOptions)Enum.Parse(typeof(GenderOptions), Gender, true),
            CountryId = CountryId,
            Address = Address,
            ReceiveNewsLetters = ReceiveNewsLetters
        };
    }
}

public static class PersonExtensions
{
    public static PersonResponse ToPersonResponse(this Person person)
    {
        return new PersonResponse()
        {
            PersonId = person.PersonId,
            PersonName = person.PersonName,
            Email = person.Email,
            DateOfBirth = person.DateOfBirth,
            Gender = person.Gender,
            CountryId = person.CountryId,
            Address = person.Address,
            ReceiveNewsLetters = person.ReceiveNewsLetters,
            Age = (person.DateOfBirth != null) ? 
                (int?)((DateTime.Now - person.DateOfBirth.Value).TotalDays/365.25) 
                : null
        };
    }
}