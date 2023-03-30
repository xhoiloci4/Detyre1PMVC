using Detyre1PMVC.Data;
using Detyre1PMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Detyre1PMVC.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BookController(ApplicationDbContext context) => _context = context;
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Libri Liber) {
            if(ModelState.IsValid)
            {
                _context.Librat.Add(Liber);
                _context.SaveChanges();
                return View("Views/Home/index.cshtml");
            }else return View("Views/Book/Create.cshtml");

        }
        [HttpGet]
        public IActionResult Edit(int id)
        { 
            Libri givenLi = _context.Librat.FirstOrDefault(p=>p.LibriId == id);
            return View(givenLi);
        }
        [HttpPost]
        public IActionResult Edit(Libri givenLiber, int id) 
        {
            if (ModelState.IsValid)
            {
                Libri? ourLiber = _context.Librat.FirstOrDefault(p => p.LibriId == id);
                if (ourLiber != null)
                {
                    ourLiber.Emri = givenLiber.Emri;
                    ourLiber.Autori = givenLiber.Autori;
                    ourLiber.DtPublikimi = givenLiber.DtPublikimi;
                    ourLiber.NrFaqe = givenLiber.NrFaqe;
                    ourLiber.KategoriId = givenLiber.KategoriId;
                    _context.SaveChanges();
                }
            }
                    return View("Views/Book/index.cshtml");
        }
    }
}
