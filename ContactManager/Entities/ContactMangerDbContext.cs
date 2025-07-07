using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using System.IO;
using MySqlConnector;

namespace Entities;

public class ContactMangerDbContext: DbContext
{
    public ContactMangerDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Country> countries { get; set; }
    public DbSet<Person>  persons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Country>().ToTable("Countries");
        modelBuilder.Entity<Person>().ToTable("Persons");
        
        //seeding counties data
        string countryJson = File.ReadAllText("Country.json");
        List<Country>? countryList = JsonSerializer.Deserialize<List<Country>>(countryJson);
        foreach (Country country in countryList!)
        {
            modelBuilder.Entity<Country>().HasData(country);
        }
        //seeding persons data
        string personJson = File.ReadAllText("Person.json");
        List<Person>? personList = JsonSerializer.Deserialize<List<Person>>(personJson);
        foreach (Person person in personList!)
        {
            modelBuilder.Entity<Person>().HasData(person);
        }
    }

    public List<Person> sp_GetAllPersons()
    {
        return persons.FromSqlRaw("CALL GetAllPersons()").ToList();
    }

    public int sp_AddPerson(Person person)
    {
        var personIdParam = new MySqlParameter("p_PersonId", person.PersonId.ToString());
        var personNameParam = new MySqlParameter("p_PersonName", person.PersonName);
        var emailParam = new MySqlParameter("p_Email", person.Email);
        var dateOfBirthParam = new MySqlParameter("p_DateOfBirth", person.DateOfBirth);
        var genderParam = new MySqlParameter("p_Gender", person.Gender);
        var countryIdParam = new MySqlParameter("p_CountryId", person.CountryId.ToString());
        var addressParam = new MySqlParameter("p_Address", person.Address);
        var receiveNewsLettersParam = new MySqlParameter("p_ReceiveNewsLetters", person.ReceiveNewsLetters);
        return Database.ExecuteSqlRaw(
                "CALL AddPerson(@p_PersonId, @p_PersonName, @p_Email, @p_DateOfBirth, @p_Gender, @p_CountryId, @p_Address, @p_ReceiveNewsLetters)",
                personIdParam,
                personNameParam,
                emailParam,
                dateOfBirthParam,
                genderParam,
                countryIdParam,
                addressParam,
                receiveNewsLettersParam);
    }

    public int sp_DeletePerson(Guid personId)
    {
        var personIdParam = new MySqlParameter("p_PersonId", personId.ToString());
        return Database.ExecuteSqlRaw("CALL DeletePerson(@p_PersonId)", personIdParam);
    }
}