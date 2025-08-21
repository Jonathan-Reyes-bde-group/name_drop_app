namespace ProjectNameDrop.Models.Response
{
    public class GenderizeGetResponse
    {
        //Reference {"count":500304,"name":"emma","gender":"female","probability":0.97}
        public int Count { get; set; }
        public string Name { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public double Probability { get; set; }
    }
}
