using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diesel.Core
{
    public class ActiveGame
    {
        public int Id { get; set; }
        public User Player1 { get; set; }
        public User Player2 { get; set; }
        public Board Board { get; set; }
        public int TurnNumber { get; set; }
        public DateTime TurnLimit { get; set; }
        public Phase Phase { get; set; }
        public MissionType MissionType { get; set; }
        public GameType GameType { get; set; }
        public DateTime GameStart { get; set; }
        public DateTime LastTime { get; set; }

    }
}
