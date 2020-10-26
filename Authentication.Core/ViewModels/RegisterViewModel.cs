using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Authentication.Core.ViewModel
{
   public class RegisterViewModel
    {

        [Display(Name = " شماره موبایل")]
        [Required(ErrorMessage = "مقدار {0}را وارد کنید")]
        [MaxLength(11, ErrorMessage = "مقدار{0} نمی تواند بیشتر از{1} باشد")]
        public string Mobile { get; set; }
        [Display(Name = "کلمه عبور")]
        [MaxLength(50, ErrorMessage = "مقدار{0} نمی تواند بیشتر از{1} باشد")]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [MaxLength(50, ErrorMessage = "مقدار{0} نمی تواند بیشتر از{1} باشد")]
        [Compare("Password", ErrorMessage ="کلمه های عبور با یک دیگر همخوانی ندارد")]
        public string RePassword { get; set; }


    }
}
