using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;

namespace Authentication.DataAccessLayer.Entities
{
     public class User
    {

        [Key]
        public int Id { get; set; }

        public int RoleId { get; set; }

        [Display(Name = " شماره موبایل")]
        [Required(ErrorMessage = "مقدار {0}را وارد کنید")]
        [MaxLength(11, ErrorMessage = "مقدار{0} نمی تواند بیشتر از{1} باشد")]
        public string Mobile { get; set; }
        [Display(Name = "کلمه عبور")]
        [MaxLength(50, ErrorMessage = "مقدار{0} نمی تواند بیشتر از{1} باشد")]
        public string Password { get; set; }
        [Display(Name = "کد فعال سازی")]

        [MaxLength(6, ErrorMessage = "مقدار{0} نمی تواند بیشتر از{1} باشد")]
        public string Code { get; set; }
        [Display(Name = "غیرفعال/فعال")]
        public bool IsActive { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role  { get; set; }


    }
}
