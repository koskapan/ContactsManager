using ContactsManager.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ContactsManager.Domain.Entity
{
    public class Contact
    {
        public int Id { get; set; }
        public Genders Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public string Phone { get; set; }
        public string AvatarUrl { get; set; }

    }

    public enum Genders { Female, Male }
}
