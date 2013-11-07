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
    public class ActiveGameRepository : IActiveGameRepository
    {
        DieselContext context = new DieselContext();

        public IQueryable<ActiveGame> All
        {
            get { return context.ActiveGames; }
        }

        public IQueryable<ActiveGame> AllIncluding(params Expression<Func<ActiveGame, object>>[] includeProperties)
        {
            IQueryable<ActiveGame> query = context.ActiveGames;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public ActiveGame Find(int id)
        {
            return context.ActiveGames.Find(id);
        }

        public void InsertOrUpdate(ActiveGame activegame)
        {
            if (activegame.Id == default(int)) {
                // New entity
                context.ActiveGames.Add(activegame);
            } else {
                // Existing entity
                context.Entry(activegame).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var activegame = context.ActiveGames.Find(id);
            context.ActiveGames.Remove(activegame);
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

    public interface IActiveGameRepository : IDisposable
    {
        IQueryable<ActiveGame> All { get; }
        IQueryable<ActiveGame> AllIncluding(params Expression<Func<ActiveGame, object>>[] includeProperties);
        ActiveGame Find(int id);
        void InsertOrUpdate(ActiveGame activegame);
        void Delete(int id);
        void Save();
    }
}