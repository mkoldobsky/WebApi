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
    public class BoardRepository : IBoardRepository
    {
        DieselContext context = new DieselContext();

        public IQueryable<Board> All
        {
            get { return context.Boards; }
        }

        public IQueryable<Board> AllIncluding(params Expression<Func<Board, object>>[] includeProperties)
        {
            IQueryable<Board> query = context.Boards;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Board Find(int id)
        {
            return context.Boards.Find(id);
        }

        public void InsertOrUpdate(Board board)
        {
            if (board.Id == default(int)) {
                // New entity
                context.Boards.Add(board);
            } else {
                // Existing entity
                context.Entry(board).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var board = context.Boards.Find(id);
            context.Boards.Remove(board);
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

    public interface IBoardRepository : IDisposable
    {
        IQueryable<Board> All { get; }
        IQueryable<Board> AllIncluding(params Expression<Func<Board, object>>[] includeProperties);
        Board Find(int id);
        void InsertOrUpdate(Board board);
        void Delete(int id);
        void Save();
    }
}