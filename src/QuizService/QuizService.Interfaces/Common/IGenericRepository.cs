using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace QuizService.Interfaces.Common
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : class
    {
        void Delete(TKey id);
        void Delete(TEntity entityToDelete);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
        TEntity GetByID(TKey id);
        void Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
    }
}