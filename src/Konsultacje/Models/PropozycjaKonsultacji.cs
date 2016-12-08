using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Konsultacje.Models
{
    public class PropozycjaKonsultacji
    {
        public int ID { get; set; }
        [Display(Name = "Pracownik uczelni")]
        public string PracownikUczelniID { get; set; }
        public ApplicationUser PracownikUczelni { get; set; }

        public DateTime Termin { get; set; }
        public string Temat { get; set; }

        public string StudentID { get; set; }
        public ApplicationUser Student { get; set; }
    }
}
