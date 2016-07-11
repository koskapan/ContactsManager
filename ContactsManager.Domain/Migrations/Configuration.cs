namespace ContactsManager.Domain.Migrations
{
    using Entity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    internal sealed class Configuration : DbMigrationsConfiguration<ContactsManager.Domain.Concrete.EfDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ContactsManager.Domain.Concrete.EfDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var appDomain = System.AppDomain.CurrentDomain;
            var basePath = appDomain.RelativeSearchPath ?? appDomain.BaseDirectory;
            Path.Combine(basePath, "MOCK_DATA.json");
            IEnumerable<Contact> contacts = JsonConvert.DeserializeObject<IEnumerable<Contact>>(File.ReadAllText(Path.Combine(basePath, "MOCK_DATA.json")));
            foreach (var c in contacts)
            {
                context.Contacts.Add(c);
            }
        }
    }
}
