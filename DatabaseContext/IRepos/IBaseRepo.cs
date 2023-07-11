using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DatabaseContext.IRepos
{
    public interface IBaseRepo<Entity> where Entity : class
    {
        public void Add(Entity entity);

        public void Update(Entity entity);

        void Delete(Entity entity); 

        IEnumerable<Entity> GetAll();

        IEnumerable<Entity> Find(Expression<Func<Entity, bool>> predicate);

        Entity Get(int id);
            
        //Entity Get(int id);

        // Get List<Entity> Where Custom Condition
    }
}
