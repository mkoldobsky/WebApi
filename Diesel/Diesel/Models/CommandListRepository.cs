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
    public class CommandListRepository : ICommandListRepository
    {
        DieselContext context = new DieselContext();

        public IQueryable<CommandList> All
        {
            get { return context.CommandLists; }
        }

        public IQueryable<CommandList> AllIncluding(params Expression<Func<CommandList, object>>[] includeProperties)
        {
            IQueryable<CommandList> query = context.CommandLists;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public CommandList Find(int id)
        {
            return context.CommandLists.Find(id);
        }

        public void InsertOrUpdate(CommandList commandlist)
        {
            if (commandlist.Id == default(int)) {
                // New entity
                context.CommandLists.Add(commandlist);
            } else {
                // Existing entity
                context.Entry(commandlist).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var commandlist = context.CommandLists.Find(id);
            context.CommandLists.Remove(commandlist);
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

    public interface ICommandListRepository : IDisposable
    {
        IQueryable<CommandList> All { get; }
        IQueryable<CommandList> AllIncluding(params Expression<Func<CommandList, object>>[] includeProperties);
        CommandList Find(int id);
        void InsertOrUpdate(CommandList commandlist);
        void Delete(int id);
        void Save();
    }
}