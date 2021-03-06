﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace C_Market.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [StringLength(30, ErrorMessage = "The field {0} must contain between {2} and {1} characteres", MinimumLength = 3)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [StringLength(30, ErrorMessage = "The field {0} must contain between {2} and {1} characteres", MinimumLength = 3)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(30, ErrorMessage = "The field {0} must contain between {2} and {1} characteres", MinimumLength = 3)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        public string Phone { get; set; }

        [StringLength(30, ErrorMessage = "The field {0} must contain between {2} and {1} characteres", MinimumLength = 3)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        public string Address { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(20, ErrorMessage = "The field {0} must contain between {2} and {1} characteres", MinimumLength = 5)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        [Display(Name = "Document")]
        public string Document { get; set; }

        public int DocumentTypeID { get; set; }

        [NotMapped]
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } set { }}

        public virtual  DocumentType DocumentType { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}