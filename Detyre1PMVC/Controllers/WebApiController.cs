using Detyre1PMVC.Data;
using Detyre1PMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Detyre1PMVC.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[Action]")]
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
        public async Task<IActionResult> GetUsers()
        {
            var nrUsers = await _context.Users.CountAsync();
            return Ok(nrUsers);
        }

        public IActionResult GetSalesInMonth()
        {
            var totalSum = (from od in _context.OrderDetails
                            join o in _context.Orders on od.OrderId equals o.OrderId
                            where o.OrderDate.Month == DateTime.Now.Month && o.OrderDate.Year == DateTime.Now.Year
                            select od.Quantity * od.UnitPrice).Sum();
            return Ok(totalSum);
        }

        public async Task<IActionResult> GetOrders()
        {
            var Orders = await _context.Orders.CountAsync();
            return Ok(Orders);
        }
        public async Task<IActionResult> Books()
        {
            var ResBooks = await _context.Librat.ToListAsync();
            return Ok(ResBooks);

        }

        public IActionResult TotalOrders()
        {
            
            var currentMonth = (from od in _context.OrderDetails
                                join o in _context.Orders on od.OrderId equals o.OrderId
                                where o.OrderDate.Month == DateTime.Now.Month && o.OrderDate.Year == DateTime.Now.Year
                                select od.Quantity * od.UnitPrice).Sum();

            var lastMonth = (from od in _context.OrderDetails
                             join o in _context.Orders on od.OrderId equals o.OrderId
                             where o.OrderDate.Month == DateTime.Now.AddMonths(-1).Month && o.OrderDate.Year == DateTime.Now.AddMonths(-1).Year
                             select od.Quantity * od.UnitPrice).Sum();
            decimal percentageChange = ((currentMonth - lastMonth) / lastMonth) * 100;
            var rezult = new
            {
                CurrentMonth = currentMonth,
                LastMonth = lastMonth,
                Percentage = percentageChange
            };

            return Ok(rezult);
        }

        public IActionResult BarChart()
        {
            var a = _context.Tests.FromSqlRaw("select MONTH(OrderDate) as labels, count(MONTH(OrderDate)) as data\r\nfrom Orders\r\nGroup by MONTH(OrderDate)").ToList();
            return Ok(a);
        }
        public IActionResult LineChart(int year)
        {
            year = (year == 0) ? DateTime.Now.Year : year;
            var a = _context.Tests.FromSqlRaw($"select MONTH(OrderDate) as labels, count(MONTH(OrderDate)) as data\r\nfrom Orders\r\nwhere YEAR(OrderDate) = {year}\r\nGroup by MONTH(OrderDate)").ToList();
            return Ok(a);
        }

    }
}
