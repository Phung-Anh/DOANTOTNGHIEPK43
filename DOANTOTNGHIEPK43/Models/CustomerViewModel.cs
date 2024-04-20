using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOANTOTNGHIEPK43.Models
{
    public class CustomerViewModel
    {
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Payment { get; set; }



    }
}

//model này để lưu trữ các thông tin khi người dung nhập vào nó là model lư trữ chứ không phỉa model trong csdl kiểu lưu trữ này giống lưu trữ đám máy