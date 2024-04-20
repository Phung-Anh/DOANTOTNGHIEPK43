using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Web;

namespace DOANTOTNGHIEPK43.Models.EF
{
    [Table("tb_ProductImage")]
    public class ProductImage // csdl chứa ảnh 
    {
       [key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Image { get; set; }
        public bool IsDefault { get; set; }

        public virtual Product Product { get; set; }
    }
}