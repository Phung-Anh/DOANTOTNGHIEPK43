﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DOANTOTNGHIEPK43.Models.EF
{
    [Table("tb_OrderDetail")]
    public class OrderDetail
    {
        [key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }     
        public int OrderId { get; set; }
        
        public int ProductId { get; set; }

        public decimal Price { get; set; }
        
        public int Quatity { get; set; }
        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }   

    }
}