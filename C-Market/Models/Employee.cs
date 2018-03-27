using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace C_Market.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage ="You must enter {0}")]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        public Decimal Salaray { get; set; }

        [Display(Name = "Bonus %")]
        public Decimal BonusPercent { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        [Display(Name = "Date of Brith")]
        public DateTime DateOfBrith { get; set; }

        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        public string Email { get; set; }

        public string URL { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        public int DocumentTypeID { get; set; }

        public virtual DocumentType DocumentType { get; set; }
    }
}