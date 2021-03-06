﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Opmas.Data.Objects.Entities.Employee
{
    public class EmployeeWorkData
    {
        public long EmployeeWorkDataId { get; set; }
        public DateTime EmploymentDate { get; set; }
        public string EmploymentStatus { get; set; }
        public long EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

        public long? EmploymentTypeId { get; set; }

        [ForeignKey("EmploymentTypeId")]
        public virtual EmploymentType EmploymentType { get; set; }

        public long? EmploymentCategoryId { get; set; }

        [ForeignKey("EmploymentCategoryId")]
        public virtual EmploymentCategory EmploymentCategory { get; set; }
        public long? EmploymentPositionId { get; set; }

        [ForeignKey("EmploymentPositionId")]
        public virtual EmploymentPosition EmploymentPosition { get; set; }
    }
}