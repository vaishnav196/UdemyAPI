using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using UdemyAPI.Data;
using UdemyAPI.Models.Chart;

namespace UdemyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public ChartController(ApplicationDbContext context)
        { 
            this.context = context;
        }

        [HttpGet("registrations-per-month")]
        public IActionResult GetRegistrationsPerMonth()
        {
            var users = context.Users.ToList();

            var registrationsPerMonth = users
                .Where(u => DateTime.TryParseExact(u.RegisteredDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                .GroupBy(u => new
                {
                    Year = DateTime.ParseExact(u.RegisteredDate, "dd-MM-yyyy", CultureInfo.InvariantCulture).Year,
                    Month = DateTime.ParseExact(u.RegisteredDate, "dd-MM-yyyy", CultureInfo.InvariantCulture).Month
                })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Count = g.Count()
                })
                .ToList();

            return Ok(registrationsPerMonth);
        }
        [HttpGet("MonthlyTotals")]
        public async Task<ActionResult<IEnumerable<MonthlyAmountViewModel>>> GetMonthlyTotals()
        {
            var data = await context.PurchaseOrder.ToListAsync();

            var monthlyTotals = data
                .Where(po => DateTime.TryParseExact(po.PurchasedDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                .GroupBy(po => DateTime.ParseExact(po.PurchasedDate, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM"))
                .Select(g => new MonthlyAmountViewModel
                {
                    Month = g.Key,
                    TotalAmount = g.Sum(x => x.Price)
                })
                .ToList();

            return Ok(monthlyTotals);
        }
    }
}
