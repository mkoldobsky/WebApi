using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diesel.Core;

namespace Diesel.DataAccess
{
    public interface IDataAccessService
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
    }
}
