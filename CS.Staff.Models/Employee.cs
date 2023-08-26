using Newtonsoft.Json;


namespace CS.Staff.Models
{
    public class Employee
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } 
        public string FirstName { get;set; } 
        public string LastName { get; set; } 
        public string Job { get; set; }
        public string Email { get; set; }
        public DateTime HireDate { get; set; } 
        public decimal Salary { get; set; }
        public List<Project> Projects { get; set; } 
        [JsonProperty("_etag")]
        public string Etag { get; set; }
    }
}
