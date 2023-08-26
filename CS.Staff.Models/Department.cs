using Newtonsoft.Json;


namespace CS.Staff.Models
{
    public class Department
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } 
        public string Name { get; set; } 
        public string Location { get; set; }
        public List<Employee> Employees { get; set; } 
        [JsonProperty("_etag")]
        public string Etag { get; set; }
    }
}
