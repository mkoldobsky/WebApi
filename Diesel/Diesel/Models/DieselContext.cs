using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Diesel.Models
{
    public class DieselContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        
        public DieselContext(): base("DefaultConnection")
        {
            
        }

        public DbSet<Diesel.Core.User> Users { get; set; }

        public DbSet<Diesel.Core.ActiveGame> ActiveGames { get; set; }

        public DbSet<Diesel.Core.Army> Armies { get; set; }

        public DbSet<Diesel.Core.Board> Boards { get; set; }

        public DbSet<Diesel.Core.BoardState> BoardStates { get; set; }

        public DbSet<Diesel.Core.ChallengeMatch> ChallengeMatches { get; set; }

        public DbSet<Diesel.Core.CommandList> CommandLists { get; set; }

        public DbSet<Diesel.Core.GameState> GameStates { get; set; }

        public DbSet<Diesel.Core.GameType> GameTypes { get; set; }

        public DbSet<Diesel.Core.MissionType> MissionTypes { get; set; }

        public DbSet<Diesel.Core.Phase> Phases { get; set; }

        public DbSet<Diesel.Core.RandomMatchQueue> RandomMatchQueues { get; set; }
    }
}