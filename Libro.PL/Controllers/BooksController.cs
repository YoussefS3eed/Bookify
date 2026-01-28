using Microsoft.AspNetCore.Mvc;

namespace Libro.PL.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View("Form");
        }
    }
}
