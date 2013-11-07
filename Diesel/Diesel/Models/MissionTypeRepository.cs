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
    public class MissionTypeRepository : IMissionTypeRepository
    {
        DieselContext context = new DieselContext();

        public IQueryable<MissionType> All
        {
            get { return context.MissionTypes; }
        }

        public IQueryable<MissionType> AllIncluding(params Expression<Func<MissionType, object>>[] includeProperties)
        {
            IQueryable<MissionType> query = context.MissionTypes;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public MissionType Find(int id)
        {
            return context.MissionTypes.Find(id);
        }

        public void InsertOrUpdate(MissionType missiontype)
        {
            if (missiontype.Id == default(int)) {
                // New entity
                context.MissionTypes.Add(missiontype);
            } else {
                // Existing entity
                context.Entry(missiontype).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var missiontype = context.MissionTypes.Find(id);
            context.MissionTypes.Remove(missiontype);
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

    public interface IMissionTypeRepository : IDisposable
    {
        IQueryable<MissionType> All { get; }
        IQueryable<MissionType> AllIncluding(params Expression<Func<MissionType, object>>[] includeProperties);
        MissionType Find(int id);
        void InsertOrUpdate(MissionType missiontype);
        void Delete(int id);
        void Save();
    }
}