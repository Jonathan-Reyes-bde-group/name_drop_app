using ProjectNameDrop.Models.Response;

namespace ProjectNameDrop.Services.Interfaces
{
    public interface INationalizeServices
    {
        /// <summary>
        /// Get the possible nationalities of a person based on their name.
        /// </summary>
        /// <param name="name">Person name</param>
        /// <returns>Object National response</returns>
        Task<NationalizeGetResponse> GetPossibleNationalAsync(string name);
    }
}
