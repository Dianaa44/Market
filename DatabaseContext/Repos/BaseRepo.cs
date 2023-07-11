using DatabaseContext.IRepos;
using DatabaseContext.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DatabaseContext.Repos
{
    public class BaseRepo<Entity> : IBaseRepo<Entity> where Entity : class
    {
        protected readonly MarketContext Context;

        private DbSet<Entity> _dbSet;
        public BaseRepo(MarketContext context)
        {
            Context = context;
            _dbSet = Context.Set<Entity>();
        }
        public void Add(Entity TEntity)
        {
            _dbSet.Add(TEntity);
        }

        public void AddRange(IEnumerable<Entity> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(Entity TEntity)
        {
            _dbSet.Remove(TEntity);
        }

        public IEnumerable<Entity> Find(Expression<Func<Entity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public Entity Get(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<Entity> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Update(Entity TEntity)
        {
            _dbSet.Update(TEntity);
        }
    }
}

/*
 which is better?
1. define Entity in DB Context (our db doesn't have such table) 
2. Implement each repository alone based on its specific Entity (repeatiton)
 */
