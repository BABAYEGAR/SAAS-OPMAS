using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opmas.Data.Service.Enums
{
    public enum EmployementCategory
    {
       
        Academic,
        [Display(Name = "Non-Academic")]
        NonAcademic,
        Administrative

    }
}
