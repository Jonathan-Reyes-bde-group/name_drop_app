using ProjectNameDrop.Models.Response;

namespace ProjectNameDrop.Services.Interfaces
{
    public interface IAgifyServices
    {
        /// <summary>
        /// Get the possible age of a person based on their name.
        /// </summary>
        /// <param name="name">Person name</param>
        /// <returns>Object age response</returns>
        Task<AgifyGetResponse> GetPossibleAgeAsync(string name);
    }
}
