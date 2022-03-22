using System;
using System.Collections.Generic;

namespace Maksgogo.Models
{
    public class User_has_film
    {
        public int Id { get; set; }
        public int? IdUser { get; set; }
        public int? IdFilm { get; set; }

        public virtual Film? IdFilmNavigation { get; set; }
        public virtual User? IdUserNavigation { get; set; }
    }
}
