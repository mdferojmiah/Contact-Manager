using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceContracts;
using ServiceContracts.DTOs;
using ServiceContracts.Enums;
using Services;

namespace ContactManager.Controllers;

[Route("persons")]
public class PersonsController : Controller
{
    //private fields
    private readonly IPersonsService _personsService;
    private readonly ICountriesService _countriesService;
    //constructor
    public PersonsController(IPersonsService personsService, ICountriesService countriesService)
    {
        _personsService = personsService;
        _countriesService = countriesService;
    }
    // GET : persons/index
    [Route("index")]
    [Route("/")]
    public IActionResult Index(string searchBy, string? searchString, string sortBy = nameof(PersonResponse.PersonName), SortOrderOptions sortOrder = SortOrderOptions.Asc)
    {
        //searching
        ViewBag.searchOptions = new Dictionary<string, string>()
        {
            {nameof(PersonResponse.PersonName), "Person Name"},
            {nameof(PersonResponse.Email), "Email"},
            {nameof(PersonResponse.Gender), "Gender"},
            {nameof(PersonResponse.DateOfBirth), "Date of Birth"},
            {nameof(PersonResponse.CountryId), "Country"},
            {nameof(PersonResponse.Address), "Address"},
        };
        List<PersonResponse> persons = _personsService.GetFilteredPersons(searchBy, searchString);
        ViewBag.currentSearchBy = searchBy;
        ViewBag.currentSearchString = searchString;
        //sorting
        List<PersonResponse> sortedPersons =  _personsService.GetSortedPersons(persons, sortBy, sortOrder);
        ViewBag.currentSortBy = sortBy;
        ViewBag.currentSortOrder = sortOrder;
        return View(sortedPersons);
    }
    //Get: persons/create
    [Route("create")]
    [HttpGet]
    public IActionResult Create()
    {
        List<CountryResponse> countries = _countriesService.GetAllCountries();
        ViewBag.Countries = countries.Select(temp => new SelectListItem()
        {
            Text = temp.CountryName,
            Value = temp.CountryId.ToString()
        });
        return View();
    }
    //Post: persons/create
    [Route("create")]
    [HttpPost]
    public IActionResult Create(PersonAddRequest personAddRequest)
    {
        if (!ModelState.IsValid)
        {
            List<CountryResponse> countries = _countriesService.GetAllCountries();
            ViewBag.Countries = countries.Select(temp => new SelectListItem()
            {
                Text = temp.CountryName,
                Value = temp.CountryId.ToString()
            });
            ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return View();
        }
        PersonResponse addedPerson = _personsService.AddPerson(personAddRequest);
        return RedirectToAction("Index", "Persons");
    }
}