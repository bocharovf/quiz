using Microsoft.EntityFrameworkCore;
using QuizService.Interfaces.Common;
using QuizService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace QuizService.DataAccess
{
    internal class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class
    {
        protected ApplicationDatabaseContext context;

        /// <summary>
        /// Prefiltered entity collection.
        /// </summary>
        protected IQueryable<TEntity> Collection => dbSet.Where(this.GlobalFilter);

        private DbSet<TEntity> dbSet;

        public GenericRepository(ApplicationDatabaseContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Gets filter expression.
        /// </summary>
        /// <param name="query">The original query.</param>
        /// <returns>Filter expression.</returns>
        protected virtual Expression<Func<TEntity, bool>> GlobalFilter { get => x => true; }

        /// <summary>
        /// Allows including properties to queries.
        /// </summary>
        /// <param name="query">The original query.</param>
        /// <returns>The query with included properties.</returns>
        protected virtual IQueryable<TEntity> IncludeProperties(IQueryable<TEntity> query)
        {
            return query;
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = this.Collection;
            query = this.IncludeProperties(query);

            if (filter != null)
            {
                query = query.Where(filter);
            }            

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetByID(TKey id)
        {
            var entity = dbSet.Find(id);
            bool isFilteredOut = !this.GlobalFilter.Compile()(entity);
            if (isFilteredOut)
            {
                return null;
            }

            return entity;
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(TKey id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }

}
