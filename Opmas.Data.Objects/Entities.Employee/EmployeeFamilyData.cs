﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Opmas.Data.Objects.Entities.SystemManagement;

namespace Opmas.Data.Objects.Entities.Employee
{
    public class EmployeeFamilyData
    {
        public long EmployeeFamilyDataId { get; set; }

        [DisplayName("Full Name")]
        [Required]
        public string FullName { get; set; }
        [Required]
        [DisplayName("Address")]
        public string Address { get; set; }
        [DisplayName("Contact Number")]
        [Required]
        public string ContactNumber { get; set; }
        [DisplayName("Email")]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Date Of Birth")]
        [Required]
        public DateTime DateOfBirth { get; set; }
        [DisplayName("Relationship")]
        [Required]
        public string Relationship { get; set; }

        public long EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

    }
}