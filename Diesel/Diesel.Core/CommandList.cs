using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diesel.Core
{
    public class CommandList
    {
        public int Id { get; set; }
        public User Player { get; set; }
        public string Commands { get; set; }
    }
}
