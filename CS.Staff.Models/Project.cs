using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace CS.Staff.Models
{
    public class Project
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } 
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }
        [JsonProperty("_etag")]
        public string Etag { get; set; }
    }
}
