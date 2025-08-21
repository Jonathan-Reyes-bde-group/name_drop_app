using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectNameDrop.Models.DTO;
using ProjectNameDrop.Services.Interfaces;

namespace ProjectNameDrop.Controllers
{
    [Route("[controller]")]
    public class MainController(
        INationalizeServices _nationalizeServices,
        IGenderizeServices _genderizeServices,
        IAgifyServices _agifyServices,
        ILogger<MainController> _logger
        ) : BaseController
    {
        [Authorize(Roles = "API_USER")]
        [HttpGet("predict-person/{name}")]
        public async Task<IActionResult> PredictPersonAttributeAsync(string name)
        {
            // Validate the name is not null or empty
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Name cannot be null or empty.");
            }

            // Validate the name length more than 2 characters
            if (name.Length < 2)
            {
                return BadRequest("Name must be at least 2 characters long.");
            }

            // Validate is the name contains only letters either with spaces
            if (!name.All(c => char.IsLetter(c) || c == ' '))
            {
                return BadRequest("Name must contain only letters.");
            }


            PersonDTO person = new();
            try
            {
                // Get possible nationalities
                var nationalResponse = await _nationalizeServices.GetPossibleNationalAsync(name);
                person.Name = name;

                var countryName = GetCountryName(nationalResponse!.Country
                    .OrderByDescending(c => c.Probability)
                    .FirstOrDefault()?.CountryId ?? "Unknown");

                person.Nationality = countryName;
                // Get possible gender
                var genderResponse = await _genderizeServices.GetPossibleGenderAsync(name);
                person.Gender = genderResponse?.Gender ?? "Unknown";
                // Get possible age
                var ageResponse = await _agifyServices.GetPossibleAgeAsync(name);
                person.Age = (int)(ageResponse?.Age ?? 0);

                return Ok(person);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while predicting person attributes for name: {Name}", name);
                throw;
            }
        }
    }
}
