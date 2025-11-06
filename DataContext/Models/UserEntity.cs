using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Models
{
    public class UserEntity
    {
        internal UserEntity(Guid id, string email, string password)
        {
            Id = id;
            Email = email;
            Password = password;
        }

        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
    }
}
