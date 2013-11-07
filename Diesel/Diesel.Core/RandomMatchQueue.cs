using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diesel.Core
{
    public class RandomMatchQueue
    {
        public int Id { get; set; }
        public User Player { get; set; }
        public string PlayerRating { get; set; }
        public GameType GameType { get; set; }

    }
}
