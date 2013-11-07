using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diesel.Core
{
    public class ChallengeMatch
    {
        public int Id { get; set; }
        public User Player1 { get; set; }
        public User Player2 { get; set; }
        public GameType GameType { get; set; }
        public DateTime TimeOfInvitation { get; set; }

    }
}
