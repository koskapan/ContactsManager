using ContactsManager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Domain.Concrete
{
    public class EfDbContext : DbContext
    {
        public EfDbContext() : base("Default") { }

        public DbSet<Contact> Contacts { get; set; }
    }
}
