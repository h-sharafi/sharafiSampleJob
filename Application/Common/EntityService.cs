using Application.Errors;
using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Application
{
    public abstract class EntityService<T> : IEntityService<T> where T : class
    {
        protected readonly DataContext _context;
        protected readonly DbSet<T> _dbset;
        public EntityService(DataContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }

        public void Create(T entity)
        {
            try
            {
                _dbset.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new RestException(HttpStatusCode.InternalServerError);

            }
        }

    }
}
