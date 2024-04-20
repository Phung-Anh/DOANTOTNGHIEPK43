using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOANTOTNGHIEPK43.Models.EF
{
    public abstract class CommonAbstract
    {
        public string  CreateBy { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

    }
}