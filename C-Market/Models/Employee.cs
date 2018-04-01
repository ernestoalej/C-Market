using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace C_Market.Models
{
    //[Table("Empleados")]  Asigna el nombre fisico a la tabla.
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        //[Column("Name")]  -- Si quiero colocarle otro nombre diferente a la columna en fisico.
        [Required(ErrorMessage ="You must enter {0}")]
        [Display(Name ="First Name")]
        [StringLength(30, ErrorMessage ="The field {0} must be between {2} and {1} characters", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(30, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 3)]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        [DisplayFormat(DataFormatString ="{0:C2}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Currency)]
        public Decimal Salaray { get; set; }

        
        [Display(Name = "Bonus %")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = false)]
        public Decimal BonusPercent { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        [Display(Name = "Date of Brith")]
        //[DisplayFormat(DataFormatString ="{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateOfBrith { get; set; }


        [Display(Name = "Start Time")]
        [DisplayFormat(DataFormatString = "{0:hh:mm}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Url)]
        public string URL { get; set; }

        //[ForeignKey("DocumentTypeID")]  - Si el nombre del campo no estuviera escrito igual en ambas tablas, debo asignar la relacion con ForeignKey.
        [Display(Name ="Document")]        
        public int DocumentTypeID { get; set; }

        //Campo calculado
        [NotMapped]
        public int Age { get { return DateTime.Now.Year - DateOfBrith.Year; } }

        public virtual DocumentType DocumentType { get; set; }
    }
}