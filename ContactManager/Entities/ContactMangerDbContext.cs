using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using System.IO;

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
}