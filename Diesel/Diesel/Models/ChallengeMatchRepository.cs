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
    public class ChallengeMatchRepository : IChallengeMatchRepository
    {
        DieselContext context = new DieselContext();

        public IQueryable<ChallengeMatch> All
        {
            get { return context.ChallengeMatches; }
        }

        public IQueryable<ChallengeMatch> AllIncluding(params Expression<Func<ChallengeMatch, object>>[] includeProperties)
        {
            IQueryable<ChallengeMatch> query = context.ChallengeMatches;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public ChallengeMatch Find(int id)
        {
            return context.ChallengeMatches.Find(id);
        }

        public void InsertOrUpdate(ChallengeMatch challengematch)
        {
            if (challengematch.Id == default(int)) {
                // New entity
                context.ChallengeMatches.Add(challengematch);
            } else {
                // Existing entity
                context.Entry(challengematch).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var challengematch = context.ChallengeMatches.Find(id);
            context.ChallengeMatches.Remove(challengematch);
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

    public interface IChallengeMatchRepository : IDisposable
    {
        IQueryable<ChallengeMatch> All { get; }
        IQueryable<ChallengeMatch> AllIncluding(params Expression<Func<ChallengeMatch, object>>[] includeProperties);
        ChallengeMatch Find(int id);
        void InsertOrUpdate(ChallengeMatch challengematch);
        void Delete(int id);
        void Save();
    }
}