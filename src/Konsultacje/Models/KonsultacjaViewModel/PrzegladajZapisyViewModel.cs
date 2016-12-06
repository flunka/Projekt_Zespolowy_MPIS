using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Konsultacje.Models.KonsultacjaViewModel
{
    public class PrzegladajZapisyViewModel
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public DateTime Termin { get; set; }
        public string Temat { get; set; }
        public int Budynek { get; set; }
        public int Sala { get; set; }
    }
}
