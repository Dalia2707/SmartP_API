namespace SmartPlant.Api.Models
{
    public class Response<T>
    {
        public bool status { get; set; }
        public string? msg { get; set; }
        public T? value { get; set; }
    }
}
