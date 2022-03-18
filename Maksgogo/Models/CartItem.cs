using System;
using System.Collections.Generic;

namespace Maksgogo
{
    public partial class CartItem
    {
        public int IdCartItems { get; set; }
        public int? IdFilm { get; set; }
        public string? Session { get; set; }

        public virtual Film? IdFilmNavigation { get; set; }
    }
}
