using ProductName.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductName.UnitTests.Fixtures
{
    public static class UsersFixtures
    {
        public static List<User> GetTestUsers() => new()
        {
            new User
            {
                Name = "Test User 1",
                Email = "test.user.1@email.com",
                Address = new Address
                {
                    Street = "123 Street st",
                    City = "Somewhere",
                    ZipCode = "213124"
                }
            },
            new User
            {
                Name = "Test User 2",
                Email = "test.user.2@email.com",
                Address = new Address
                {
                    Street = "321 Street st",
                    City = "Anywhere",
                    ZipCode = "1234123"
                }
            },
            new User
            {
                Name = "Test User 3",
                Email = "test.user.3@email.com",
                Address = new Address
                {
                    Street = "111 Street st",
                    City = "Nowhere",
                    ZipCode = "00000"
                }
            },
        };
    }
}
