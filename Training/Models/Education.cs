using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Training.Models
{
    public class Education
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String Degree { get; set; }
        [Required]
        public String GPA { get; set; }
        [ForeignKey("University")]
        public int University_Id { get; set; }

        public virtual University University { get; set; }

    }
}
