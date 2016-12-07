using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Konsultacje.Models
{
    public class PropozycjaKonsultacji
    {
        public int ID { get; set; }
        public string PracownikUczelniID { get; set; }
        public ApplicationUser PracownikUczelni { get; set; }

        public DateTime Termin { get; set; }
        public string Temat { get; set; }

        public string StudentID { get; set; }
        public ApplicationUser Student { get; set; }
    }
}
