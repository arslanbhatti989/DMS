using DMS.Data;
using Microsoft.AspNetCore.Mvc;

namespace DMS.Controllers
{
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AccountsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
