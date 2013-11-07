using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diesel.Core
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FacebookId { get; set; }
        public string TwitterId { get; set; }
        public string Rating { get; set; }
        public int Won { get; set; }
        public int Lost { get; set; }
        public string Purchases { get; set; }
        public int MaxActiveGames { get; set; }
        public List<User> Friends { get; set; }
    }
}
