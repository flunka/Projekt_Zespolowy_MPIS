﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Konsultacje.Models.KonsultacjaViewModel
{
    public class PrzegladajKonsultacjeViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Pracownik Uczelni")]
        public string DisplayName { get; set; }
        public string IdPracownika { get; set; }
        public int Budynek { get; set; }
        public int Sala { get; set; }
        public DateTime Termin { get; set; }
    }
}
