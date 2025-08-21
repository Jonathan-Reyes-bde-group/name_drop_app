using ProjectNameDrop.Models.Response;

namespace ProjectNameDrop.Services.Interfaces
{
    public interface IGenderizeServices
    {
        /// <summary>
        /// Get the possible gender by name
        /// </summary>
        /// <param name="name">Person name</param>
        /// <returns>Object gender response</returns>
        Task<GenderizeGetResponse> GetPossibleGenderAsync(string name);
    }
}
