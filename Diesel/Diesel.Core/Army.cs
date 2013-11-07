using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diesel.Core
{
    public class Army
    {
        public int Id { get; set; }
        public User Player { get; set; }
        public string Nation { get; set; }
        public string Units { get; set; }
    }
}
