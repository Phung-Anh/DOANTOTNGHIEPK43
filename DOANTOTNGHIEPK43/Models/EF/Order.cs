using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DOANTOTNGHIEPK43.Models.EF
{
    [Table("tb_Order")]
    public class Order : CommonAbstract
    {
        public Order ()
        {
            this.OrderDetails = new HashSet<OrderDetail> ();    
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Code { get; set; }
        [Required(ErrorMessage = "Tên khách hàng không để trống")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Số điện thoại không để trống")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Địa chỉ không để trống")]
        public string Address { get; set; }
        public string Email { get; set; }
        public string TotalAmount { get ; set; }
        public int Quatity { get; set; }
        public int TypePayment { get; set; }


        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    }
}