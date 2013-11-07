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
    public class GameStateRepository : IGameStateRepository
    {
        DieselContext context = new DieselContext();

        public IQueryable<GameState> All
        {
            get { return context.GameStates; }
        }

        public IQueryable<GameState> AllIncluding(params Expression<Func<GameState, object>>[] includeProperties)
        {
            IQueryable<GameState> query = context.GameStates;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public GameState Find(int id)
        {
            return context.GameStates.Find(id);
        }

        public void InsertOrUpdate(GameState gamestate)
        {
            if (gamestate.Id == default(int)) {
                // New entity
                context.GameStates.Add(gamestate);
            } else {
                // Existing entity
                context.Entry(gamestate).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var gamestate = context.GameStates.Find(id);
            context.GameStates.Remove(gamestate);
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

    public interface IGameStateRepository : IDisposable
    {
        IQueryable<GameState> All { get; }
        IQueryable<GameState> AllIncluding(params Expression<Func<GameState, object>>[] includeProperties);
        GameState Find(int id);
        void InsertOrUpdate(GameState gamestate);
        void Delete(int id);
        void Save();
    }
}