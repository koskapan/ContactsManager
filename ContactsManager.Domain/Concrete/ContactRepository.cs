﻿using ContactsManager.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsManager.Domain.Entity;
using System.Linq.Expressions;

namespace ContactsManager.Domain.Concrete
{
    public class ContactRepository : IContactRepository
    {
        EfDbContext context;

        public ContactRepository()
        {
            context = new EfDbContext();
        }

        public IQueryable<Contact> AsQueryable()
        {
            return context.Contacts.AsQueryable();
        }

        public void Create(Contact entity)
        {
            context.Contacts.Add(entity);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var contactForDelete = context.Contacts.Find(id);
            if (contactForDelete != null)
            {
                context.Contacts.Remove(contactForDelete);
                context.SaveChanges();
            }
        }

        public void Edit(int id, Contact editObject)
        {
            if (editObject.Id == id)
            {
                context.Entry(editObject).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public IEnumerable<Contact> Get()
        {
            return context.Contacts;
        }

        public Contact Get(int id)
        {
            return context.Contacts.Find(id);
        }
    }
}
