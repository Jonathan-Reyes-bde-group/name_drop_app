namespace ProjectNameDrop.Models.Response
{
    public class AgifyGetResponse
    {
        //Reference: {"count":55678,"name":"Jeremy","age":47}
        public int Count { get; set; }
        public string Name { get; set; } = null!;
        public int Age { get; set; }
    }
}
