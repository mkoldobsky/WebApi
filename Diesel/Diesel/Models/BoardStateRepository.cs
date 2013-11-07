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
    public class BoardStateRepository : IBoardStateRepository
    {
        DieselContext context = new DieselContext();

        public IQueryable<BoardState> All
        {
            get { return context.BoardStates; }
        }

        public IQueryable<BoardState> AllIncluding(params Expression<Func<BoardState, object>>[] includeProperties)
        {
            IQueryable<BoardState> query = context.BoardStates;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public BoardState Find(int id)
        {
            return context.BoardStates.Find(id);
        }

        public void InsertOrUpdate(BoardState boardstate)
        {
            if (boardstate.Id == default(int)) {
                // New entity
                context.BoardStates.Add(boardstate);
            } else {
                // Existing entity
                context.Entry(boardstate).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var boardstate = context.BoardStates.Find(id);
            context.BoardStates.Remove(boardstate);
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

    public interface IBoardStateRepository : IDisposable
    {
        IQueryable<BoardState> All { get; }
        IQueryable<BoardState> AllIncluding(params Expression<Func<BoardState, object>>[] includeProperties);
        BoardState Find(int id);
        void InsertOrUpdate(BoardState boardstate);
        void Delete(int id);
        void Save();
    }
}