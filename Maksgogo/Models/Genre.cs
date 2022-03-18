using System;
using System.Collections.Generic;

namespace Maksgogo
{
    public partial class Genre
    {
        public Genre()
        {
            Films = new HashSet<Film>();
        }

        public int IdGenre { get; set; }
        public string? GenreName { get; set; }

        public virtual ICollection<Film> Films { get; set; }
    }
}
