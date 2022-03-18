using System;
using System.Collections.Generic;

namespace Maksgogo
{
    public partial class User
    {
        public User()
        {
            OrderCarts = new HashSet<OrderCart>();
        }

        public int IdUser { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Session { get; set; }

        public virtual ICollection<OrderCart> OrderCarts { get; set; }
    }
}
