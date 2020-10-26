using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Authentication.Core.ViewModels
{
    public class LoginViewModel
    {


        [Display(Name = " شماره موبایل")]
        [Required(ErrorMessage = "مقدار {0}را وارد کنید")]
        [MaxLength(11, ErrorMessage = "مقدار{0} نمی تواند بیشتر از{1} باشد")]
        public string Mobile { get; set; }
        [Display(Name = "کلمه عبور")]
        [MaxLength(50, ErrorMessage = "مقدار{0} نمی تواند بیشتر از{1} باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
