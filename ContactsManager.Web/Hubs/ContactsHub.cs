using ContactsManager.Domain.Entity;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ContactsManager.Web.Hubs
{
    public class ContactsHub: Hub
    {
        IHubContext context;

        public ContactsHub()
        {
            context = GlobalHost.ConnectionManager.GetHubContext<ContactsHub>();
        }

        public void AddContact(Contact data)
        {
            context.Clients.All.addData(data);
        }

        public void RemoveContact(int id)
        {
            context.Clients.All.removeData(id);
        }

        public void EditContact(int id, Contact data)
        {
            context.Clients.All.editData(id, data);
        }
    }
}