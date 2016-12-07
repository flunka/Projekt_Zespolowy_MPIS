using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Konsultacje.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} musi mieć do {2} do {1} znaków.", MinimumLength = 1)]        
        [Display(Name = "Imię")]
        public string Imie { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi mieć do {2} do {1} znaków.", MinimumLength = 1)]
        [Display(Name = "Nazwisko")]
        public string Nazwisko { get; set; }

        [Required]
        [StringLength(1, ErrorMessage = "{0} musi mieć do {2} do {1} znaków.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Typ konta")]
        public string TypKonta { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi mieć do {2} do {1} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
