using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Konsultacje.Data;
using Konsultacje.Models;
using Konsultacje.Models.KonsultacjaViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Konsultacje.Controllers
{
    [Authorize(Roles = "Student, PracownikUczelni")]
    public class ZapisNaKonsultacjeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public ZapisNaKonsultacjeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ZapisNaKonsultacje
        [Authorize(Roles = "PracownikUczelni")]
        public async Task<IActionResult> Index()
        {
            var pracownik = _context.Users.Single(user => user.Id == _userManager.GetUserId(User));

            var query = from c in _context.ZapisNaKonsultacje.ToList()
                        join d in _context.Users.ToList() on c.StudentID equals d.Id
                        join f in _context.Konsultacja.ToList() on c.KonsultacjaID equals f.ID
                        where f.PracownikUczelniID == pracownik.Id
                        select new PrzegladajZapisyViewModel
                        {
                            Id = c.ID,
                            DisplayName = d.DisplayName,
                            Temat = c.Temat,
                            Budynek = f.Budynek,
                            Sala = f.Sala,
                            Termin = f.Termin
                        };
                        

            return View(query);
        }

        // GET: ZapisNaKonsultacje/Details/5
        [Authorize(Roles = "PracownikUczelni, Student")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zapisNaKonsultacje = await _context.ZapisNaKonsultacje.SingleOrDefaultAsync(m => m.ID == id);
            var konsultacja = await _context.Konsultacja.SingleOrDefaultAsync(m => m.ID == zapisNaKonsultacje.KonsultacjaID);
            var student = await _context.Users.SingleOrDefaultAsync(m => m.Id == zapisNaKonsultacje.StudentID);
            if (zapisNaKonsultacje == null)
            {
                return NotFound();
            }

            var model = new PrzegladajZapisyViewModel
            {
                Id = zapisNaKonsultacje.ID,
                DisplayName = student.DisplayName,
                Temat = zapisNaKonsultacje.Temat,
                Termin = konsultacja.Termin,
                Budynek = konsultacja.Budynek,
                Sala = konsultacja.Sala
            };

            return View(model);
        }

        // GET: ZapisNaKonsultacje/Create
        [Authorize(Roles = "Student")]
        public IActionResult Create(int id)
        {
            ViewData["IdKonsultacji"] = id;
            return View();
        }

        // POST: ZapisNaKonsultacje/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Student")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Temat,KonsultacjaID")] ZapisNaKonsultacje zapisNaKonsultacje)
        {
            if (ModelState.IsValid)
            {
                if (!SaWolneMiejsca(zapisNaKonsultacje.KonsultacjaID))
                {
                    return RedirectToAction("BrakMiejsc");//TODO: Trzeba wyświetlać info o braku miejsc na konsultacje
                }
                var student = _context.Users.Single(user => user.Id == _userManager.GetUserId(User));
                zapisNaKonsultacje.Student = student;
                _context.Add(zapisNaKonsultacje);
                await _context.SaveChangesAsync();
                return RedirectToAction("Student");
            }
            return View(zapisNaKonsultacje);
        }

        // GET: ZapisNaKonsultacje/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zapisNaKonsultacje = await _context.ZapisNaKonsultacje.SingleOrDefaultAsync(m => m.ID == id);
            if (zapisNaKonsultacje == null)
            {
                return NotFound();
            }
            return View(zapisNaKonsultacje);
        }

        // POST: ZapisNaKonsultacje/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Temat,KonsultacjaID,StudentID")] ZapisNaKonsultacje zapisNaKonsultacje)
        {
            if (id != zapisNaKonsultacje.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zapisNaKonsultacje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZapisNaKonsultacjeExists(zapisNaKonsultacje.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Student");
            }
            return View(zapisNaKonsultacje);
        }

        // GET: ZapisNaKonsultacje/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zapisNaKonsultacje = await _context.ZapisNaKonsultacje.SingleOrDefaultAsync(m => m.ID == id);
            if (zapisNaKonsultacje == null)
            {
                return NotFound();
            }

            return View(zapisNaKonsultacje);
        }

        // POST: ZapisNaKonsultacje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zapisNaKonsultacje = await _context.ZapisNaKonsultacje.SingleOrDefaultAsync(m => m.ID == id);
            _context.ZapisNaKonsultacje.Remove(zapisNaKonsultacje);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: ZapisNaKonsultacje/Student
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Student()
        {
            var student = _context.Users.Single(user => user.Id == _userManager.GetUserId(User));

            var query = from c in _context.ZapisNaKonsultacje.ToList()
                        join f in _context.Konsultacja.ToList() on c.KonsultacjaID equals f.ID
                        join g in _context.Users.ToList() on f.PracownikUczelniID equals g.Id
                        where c.StudentID == student.Id
                        select new PrzegladajZapisyViewModel
                        {
                            Id = c.ID,
                            DispalyName2 = g.DisplayName,
                            Temat = c.Temat,
                            Budynek = f.Budynek,
                            Sala = f.Sala,
                            Termin = f.Termin
                        };


            return View(query);
        }

        public async Task<IActionResult> BrakMiejsc()
        {
            return View();
        }

        public async Task<IActionResult> PowrotLista()
        {
            return RedirectToAction("Index","Konsultacja",null);
        }



        private bool ZapisNaKonsultacjeExists(int id)
        {
            return _context.ZapisNaKonsultacje.Any(e => e.ID == id);
        }

        private bool konsultacjaMaLimit(int idKonsultacji) {
            return _context.Konsultacja.Single(konsultacja => konsultacja.ID == idKonsultacji).Limit > 0;
        }

        private int policzZapisanychNaKonsultacje(int idKonsultacji) {
            return _context.ZapisNaKonsultacje.Count(m => m.KonsultacjaID == idKonsultacji);
        }

        private bool SaWolneMiejsca(int idKonsultacji) {
            if (konsultacjaMaLimit(idKonsultacji))
            {
                var liczba = policzZapisanychNaKonsultacje(idKonsultacji);
                if (liczba >= _context.Konsultacja.Single(m => m.ID == idKonsultacji).Limit)
                {
                    return false;
                }
                else return true;
            }
            return true;
        }
    }
}
