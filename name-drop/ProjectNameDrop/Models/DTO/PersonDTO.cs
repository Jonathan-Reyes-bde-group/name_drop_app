using ProjectNameDrop.Models.Response;

namespace ProjectNameDrop.Models.DTO
{
    public class PersonDTO
    {
        public string Name { get; set; } = null!;
        public string Nationality { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public int Age { get; set; }

    }
}
