using Maksgogo.Models;
using System;
using System.Collections.Generic;

namespace Maksgogo
{
    public partial class Film
    {
        public Film()
        {
            CartItems = new HashSet<CartItem>();
            User_has_films = new HashSet<User_has_film>();
            OrderInfos = new HashSet<OrderInfo>();
        }

        public int IdFilm { get; set; }
        public int? IdGenre { get; set; }
        public int? IdStudio { get; set; }
        public string? Name { get; set; }
        public string? Descr { get; set; }
        public string? Image { get; set; }
        public string? Country { get; set; }
        public int? Price { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public bool? IsFav { get; set; }
        public int? AmountBougth { get; set; }

        public string Href { get; set; }

        public virtual Genre? IdGenreNavigation { get; set; }
        public virtual Studio? IdStudioNavigation { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<OrderInfo> OrderInfos { get; set; }
        public virtual ICollection<User_has_film> User_has_films { get; set; }
    }
}
