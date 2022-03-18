using System;
using System.Collections.Generic;

namespace Maksgogo
{
    public partial class OrderCart
    {
        public int IdOrderCart { get; set; }
        public int? IdUser { get; set; }

        public virtual User? IdUserNavigation { get; set; }
    }
}
