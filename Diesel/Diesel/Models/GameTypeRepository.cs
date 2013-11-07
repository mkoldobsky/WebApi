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
    public class GameTypeRepository : IGameTypeRepository
    {
        DieselContext context = new DieselContext();

        public IQueryable<GameType> All
        {
            get { return context.GameTypes; }
        }

        public IQueryable<GameType> AllIncluding(params Expression<Func<GameType, object>>[] includeProperties)
        {
            IQueryable<GameType> query = context.GameTypes;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public GameType Find(int id)
        {
            return context.GameTypes.Find(id);
        }

        public void InsertOrUpdate(GameType gametype)
        {
            if (gametype.Id == default(int)) {
                // New entity
                context.GameTypes.Add(gametype);
            } else {
                // Existing entity
                context.Entry(gametype).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var gametype = context.GameTypes.Find(id);
            context.GameTypes.Remove(gametype);
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

    public interface IGameTypeRepository : IDisposable
    {
        IQueryable<GameType> All { get; }
        IQueryable<GameType> AllIncluding(params Expression<Func<GameType, object>>[] includeProperties);
        GameType Find(int id);
        void InsertOrUpdate(GameType gametype);
        void Delete(int id);
        void Save();
    }
}