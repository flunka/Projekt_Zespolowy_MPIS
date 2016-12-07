using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Konsultacje.Models.KonsultacjaViewModel
{
    public class PrzegladajZapisyViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Student")]
        public string DisplayName { get; set; }
        [Display(Name ="Pracownik Uczelni")]
        public string DispalyName2 { get; set; }
        public DateTime Termin { get; set; }
        public string Temat { get; set; }
        public int Budynek { get; set; }
        public int Sala { get; set; }
    }
}
