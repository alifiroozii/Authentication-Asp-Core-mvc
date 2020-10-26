using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Text;


namespace Authentication.DataAccessLayer.Entities
{
   public class Role
    { 
        [Key]
        public int Id { get; set; }


        [Display(Name="نام نقش")]

        [Required(ErrorMessage ="مقدار {0}را وارد کنید")]

        [MaxLength(20,ErrorMessage ="مقدار{0} نمی تواند بیشتر از{1} باشد")]
        public string Name { get; set; }

        public virtual  ICollection<User> Users { get; set; }

    }
}