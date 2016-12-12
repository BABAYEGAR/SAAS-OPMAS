﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Opmas.Data.Objects.Entities.AccessManagement;

namespace Opmas.Data.Objects.Entities.SystemManagement
{
    public class Institution
    {
        public long InstitutionId { get; set; }
        [Required]
        [DisplayName("Institution Name")]
        public string Name { get; set; }
        public string Motto { get; set; }
        public string Logo { get; set; }
        public string Location { get; set; }
        [Required]
        [DisplayName("Contact Number")]
        public string ContactNumber { get; set; }
        [Required]
        [EmailAddress]
        [DisplayName("Contact Email")]
        public string ContactEmail { get; set; }
        public IEnumerable<Faculty> Faculties { get; set; }
        public IEnumerable<Department> Departments { get; set; }
        [DisplayName("Package")]
        public long PackageId { get; set; }

        [ForeignKey("PackageId")]
        public virtual Package Packages { get; set; }
    }
}