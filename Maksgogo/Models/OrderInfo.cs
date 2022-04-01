namespace Maksgogo.Models
{
    public class OrderInfo
    {
        public int IdOrderInfo { get; set; }
        public int IdOrder { get; set; }

        public int IdFilm { get; set; }

        public virtual Film? IdFilmNavigation { get; set; }
        public virtual Order? IdOrderNavigation { get; set; }
    }
}
