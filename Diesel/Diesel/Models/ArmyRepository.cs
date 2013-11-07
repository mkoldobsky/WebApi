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
    public class ArmyRepository : IArmyRepository
    {
        DieselContext context = new DieselContext();

        public IQueryable<Army> All
        {
            get { return context.Armies; }
        }

        public IQueryable<Army> AllIncluding(params Expression<Func<Army, object>>[] includeProperties)
        {
            IQueryable<Army> query = context.Armies;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Army Find(int id)
        {
            return context.Armies.Find(id);
        }

        public void InsertOrUpdate(Army army)
        {
            if (army.Id == default(int)) {
                // New entity
                context.Armies.Add(army);
            } else {
                // Existing entity
                context.Entry(army).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var army = context.Armies.Find(id);
            context.Armies.Remove(army);
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

    public interface IArmyRepository : IDisposable
    {
        IQueryable<Army> All { get; }
        IQueryable<Army> AllIncluding(params Expression<Func<Army, object>>[] includeProperties);
        Army Find(int id);
        void InsertOrUpdate(Army army);
        void Delete(int id);
        void Save();
    }
}