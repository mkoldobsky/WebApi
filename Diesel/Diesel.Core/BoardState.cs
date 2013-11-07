using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diesel.Core
{
    public class BoardState
    {
        public int Id { get; set; }
        public User Player { get; set; }
        public string State { get; set; }
    }
}
