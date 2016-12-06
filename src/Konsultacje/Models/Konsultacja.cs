using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Konsultacje.Models
{
    public class Konsultacja
    {
        public int ID { get; set; }
        public string PracownikUczelniID { get; set; }
        public ApplicationUser PracownikUczelni { get; set; }
        public DateTime Termin { get; set; }
        public int Sala { get; set; }
        public int Budynek { get; set; }

        public List<ZapisNaKonsultacje> ZapisNaKonsultacje { get; set; }
    }
}
