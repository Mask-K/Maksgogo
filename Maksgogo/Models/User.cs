using Maksgogo.Models;
using System;
using System.Collections.Generic;

namespace Maksgogo
{
    public partial class User
    {
        public User()
        {
            User_has_films = new HashSet<User_has_film>();
            Order = new HashSet<Order>();

        }

        public int IdUser { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Session { get; set; }

        public virtual ICollection<User_has_film> User_has_films { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
