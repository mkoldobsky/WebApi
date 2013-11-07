using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Diesel.Core;

namespace Diesel.Models
{ 
    public class RandomMatchQueueRepository : IRandomMatchQueueRepository
    {
        DieselContext context = new DieselContext();

        public IQueryable<RandomMatchQueue> All
        {
            get { return context.RandomMatchQueues; }
        }

        public IQueryable<RandomMatchQueue> AllIncluding(params Expression<Func<RandomMatchQueue, object>>[] includeProperties)
        {
            IQueryable<RandomMatchQueue> query = context.RandomMatchQueues;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public RandomMatchQueue Find(int id)
        {
            return context.RandomMatchQueues.Find(id);
        }

        public void InsertOrUpdate(RandomMatchQueue randommatchqueue)
        {
            if (randommatchqueue.Id == default(int)) {
                // New entity
                context.RandomMatchQueues.Add(randommatchqueue);
            } else {
                // Existing entity
                context.Entry(randommatchqueue).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var randommatchqueue = context.RandomMatchQueues.Find(id);
            context.RandomMatchQueues.Remove(randommatchqueue);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
        }
    }

    public interface IRandomMatchQueueRepository : IDisposable
    {
        IQueryable<RandomMatchQueue> All { get; }
        IQueryable<RandomMatchQueue> AllIncluding(params Expression<Func<RandomMatchQueue, object>>[] includeProperties);
        RandomMatchQueue Find(int id);
        void InsertOrUpdate(RandomMatchQueue randommatchqueue);
        void Delete(int id);
        void Save();
    }
}