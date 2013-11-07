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
    public class PhaseRepository : IPhaseRepository
    {
        DieselContext context = new DieselContext();

        public IQueryable<Phase> All
        {
            get { return context.Phases; }
        }

        public IQueryable<Phase> AllIncluding(params Expression<Func<Phase, object>>[] includeProperties)
        {
            IQueryable<Phase> query = context.Phases;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Phase Find(int id)
        {
            return context.Phases.Find(id);
        }

        public void InsertOrUpdate(Phase phase)
        {
            if (phase.Id == default(int)) {
                // New entity
                context.Phases.Add(phase);
            } else {
                // Existing entity
                context.Entry(phase).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var phase = context.Phases.Find(id);
            context.Phases.Remove(phase);
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

    public interface IPhaseRepository : IDisposable
    {
        IQueryable<Phase> All { get; }
        IQueryable<Phase> AllIncluding(params Expression<Func<Phase, object>>[] includeProperties);
        Phase Find(int id);
        void InsertOrUpdate(Phase phase);
        void Delete(int id);
        void Save();
    }
}