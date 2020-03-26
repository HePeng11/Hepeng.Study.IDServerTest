using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hepeng.Study.IDServerTest.Models
{
    public interface IUserRepository
    {
        Task<User> FindAsync(string userName);
        Task<User> FindAsync(long userId);

        bool ValidateCredentials(string name,string pswd);
    }

    public class UserRepository : IUserRepository
    {
        public Task<User> FindAsync(string userName)
        {
            return Task.FromResult(new User()
            {
                Firstname = userName,
                UserId = 123,
                IsActive = true,
                Role = "admin"
            });
        }

        public Task<User> FindAsync(long userId)
        {
            return Task.FromResult(new User()
            {
                Firstname = "",
                UserId = userId,
                IsActive = true,
                Role = "admin"
            });
        }

        public bool ValidateCredentials(string name, string pswd)
        {
            return true;
        }
    }

    public class User
    {
        public long UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Some_Data_From_User { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }

        public bool IsActive { get; set; }

    }
}
