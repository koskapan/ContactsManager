using ContactsManager.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ContactsManager.Domain.Entity
{
    [JsonObject]
    public class Contact
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("gender")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Genders Gender { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("company_name")]
        public string CompanyName { get; set; }
        [JsonProperty("job_title")]
        public string JobTitle { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("avatar")]
        public string AvatarUrl { get; set; }

    }

    public enum Genders { Female, Male }
}
