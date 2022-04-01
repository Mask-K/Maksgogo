using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Maksgogo.Models
{
    public class Order
    {
        public Order()
        {
            OrderInfos = new HashSet<OrderInfo>();
        }
        [BindNever]
        public int IdOrder { get; set; }
        [Required(ErrorMessage ="Не коректно введено ім'я")]
        public string Name { get; set; } = null!;
        
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Не коректно введено пошту")]

        public string Email { get; set; } = null!;

        [BindNever]
        [ScaffoldColumn(false)]
        public DateTime OrderTime { get; set; }

        public int? IdUser { get; set; }


        public virtual User? idUserNavigation { get; set; }
        public virtual ICollection<OrderInfo> OrderInfos { get; set; }

    }
}
