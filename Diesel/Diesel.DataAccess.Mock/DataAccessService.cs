using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diesel.Core;

namespace Diesel.DataAccess.Mock
{
    public class DataAccessService : IDataAccessService
    {
        public IEnumerable<User> GetAllUsers()
        {
            return new User [] {GetTestUser1(), GetTestUser2()};

        }

        public User GetUserById(int id)
        {
            return GetTestUser1();
        }

        private User GetTestUser1()
        {
            _user1.Friends = new List<User> {_user2};
            return _user1;
        }

        private User GetTestUser2()
        {
            _user2.Friends = new List<User>{_user1};
            return _user2;
        }


        User _user1 = new User{
            Id = 1,
            Username = "Test",
            Password = "Passw0rd!",
            FacebookId = "test",
            TwitterId = "Test",
            Won = 10,
            Lost = 20,
            MaxActiveGames = 2,
            Rating = "1",
            Purchases = "one|three",
        };
        User _user2 = new User
        {
            Id = 1,
            Username = "Friend",
            Password = "Passw0rd!",
            FacebookId = "friend",
            TwitterId = "friend",
            Won = 20,
            Lost = 10,
            MaxActiveGames = 5,
            Rating = "5",
            Purchases = "two |three",
        };

    }
}
