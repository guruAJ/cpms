using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO.Models
{
    
    public class Login
    {
        [Key]
        public int Id { get; set; }
       
        public int RoleType { get; set; }
        [Column(TypeName = "varchar(200)")]
        [Required]

        [Display(Name = "User Id")]
        public string UserID { get; set; }
        [Column(TypeName = "varchar(200)")]
     
        public string Name { get; set; }
        [Column(TypeName = "varchar(200)")]
      
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        public int ComponentId { get; set; }

        [Display(Name = "Rank Id")]
        public int RankId { get; set; }

        [NotMapped]
        [Display(Name = "Rank")]
        public string RankName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int IsActive { get; set; }
        public string CreatedBy { get; set; }
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }

        public string IdAndName
        {
            get
            {
                return string.Format("{0} {1}", RankName, UserID);
            }
        }
    }
    
}
