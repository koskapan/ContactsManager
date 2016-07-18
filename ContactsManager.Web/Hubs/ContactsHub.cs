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
        public void Send(Contact entity)
        {
            Clients.AllExcept(Context.ConnectionId).sendData(entity);
        }
    }
}