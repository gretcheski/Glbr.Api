﻿using Glbr.Domain.Contracts.Repositories;
using Glbr.Domain.Entities;
using Glbr.Infra.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Glbr.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private GlbrDataContext _context;

        public UserRepository(GlbrDataContext context)
        {
            this._context = context;
        }

        public User Get(string email)
        {
            return _context.Users.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault();
        }

        public User Get(Guid id)
        {
            return _context.Users.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<User> Get(int skip, int take)
        {
            return _context.Users.OrderBy(x => x.Name).Skip(skip).Take(take).ToList();
        }

        public void Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _context.Entry<User>(user).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
