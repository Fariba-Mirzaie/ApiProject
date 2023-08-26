using Domain.Data;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly MyContext _myContext;
        private DbSet<T> entities;

        public Repository(MyContext context)
        {
            _myContext = context;
            entities = _myContext.Set<T>();
        }

        //Methods
        public IEnumerable<T> GetAll()
        {
            return entities.ToList();
        }

        public T GetById(int id)
        {
            var entity = entities.AsNoTracking().FirstOrDefault(p => p.Id == id);
            return entity;

        }

        public bool Insert(T entity)
        {
            entities.Add(entity);
            SaveChanges();
            return true;
        }

        public bool Update(T entity)
        {
            entities.Update(entity);
            SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
            var entity = GetById(id);
            entities.Remove(entity);
            SaveChanges();
            return true;
        }

        public void SaveChanges()
        {
            _myContext.SaveChanges();
        }
    }
}
