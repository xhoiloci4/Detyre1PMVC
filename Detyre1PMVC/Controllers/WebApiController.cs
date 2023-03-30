using Detyre1PMVC.Data;
using Detyre1PMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Detyre1PMVC.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class WebApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public WebApiController(ApplicationDbContext context) => _context = context;

        //Te merren informacionet per Librat nga Autori: Ismail Kadare dhe jane me te vjeter se viti 2010 (Autori le te na vije si parameter)
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Libri>))]
        public  IActionResult Index(string Emri)
        {
            if (String.IsNullOrEmpty(Emri) || Emri == "*")
            {
                return Ok(_context.Librat.OrderBy(p => p.Emri));
            }
            else
            {
                return Ok(_context.Librat.Where(p=> p.Emri.Contains(Emri)).OrderBy(p => p.Emri));
            }
        }

        //Modifikimi i kategorise se Librave te botuar pas Viteve 2000, Viti vjen si parameter 

        [HttpPut("{Viti}")]
        public IActionResult Edit(int Viti) 
        {
            var KategoriEmri = _context.Kategorite.FirstOrDefault(p => p.Emri == "Millenium").KategoriaId;
            DateTime test = new(Viti, 1, 1);
            _context.Database.ExecuteSqlRaw("Update Librat Set KategoriId = {0} Where DtPublikimi >{1}",KategoriEmri, test);
            return NoContent();
        }

        //Fshirja e nje libri 
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var given = _context.Librat.Find(id);
            if (given == null)
            {
                return BadRequest("Book not found");
            }
            _context.Librat.Remove(given);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
