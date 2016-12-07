using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Konsultacje.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string TypKonta { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        [Display(Name = "Pracownik uczelni")]
        public string DisplayName { get; set; }

        public List<ZapisNaKonsultacje> ZapisNaKonsultacje { get; set; }
        public List<Konsultacja> Konsultacje { get; set; }
        public List<PropozycjaKonsultacji> PropozycjeKonsultacji { get; set; }
        public List<PropozycjaKonsultacji> PropozycjeKonsultacji2 { get; set; }

    }
}
