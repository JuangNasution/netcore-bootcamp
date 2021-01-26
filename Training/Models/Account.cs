using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Training.Models
{
    public class Account
    {
        [Key]
        public String NIK { get; set; }

        [Required]
        public String Password { get; set; }

        public virtual Person Person { get; set; }

        public virtual Profiling Profiling { get; set; }

    }
}
