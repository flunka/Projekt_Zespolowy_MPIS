using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Konsultacje.Models
{
    public class ZapisNaKonsultacje
    {
        public int ID { get; set; }        
        public string Temat { get; set; }

        public string StudentID { get; set; }
        public ApplicationUser Student { get; set; }

        public int KonsultacjaID { get; set; }
        public Konsultacja Konsultacja { get; set; }
    }
}
