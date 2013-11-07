using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diesel.Core;
using Diesel.Model;

namespace Diesel.DataAccess
{
    public class DataAccess : IDataAccessService
    {
        public IEnumerable<User> GetAllUsers()
        {
            using (var context = new DieselContext())
            {
                
            }
        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
