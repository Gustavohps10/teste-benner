using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microwave_benner.Domain.Entities
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public User(int id, string userName, string password)
            : this(userName, password)
        {
            this.id = id;
        }
    }
}
