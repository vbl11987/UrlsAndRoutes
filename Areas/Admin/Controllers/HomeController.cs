using Microsoft.AspNetCore.Mvc;
using UrlsAndRoutes.Areas.Admin.Models;

namespace UrlsAndRoutes.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private Person[] data = new Person[] {
            new Person { Name = "Joey", City = "London" },
            new Person { Name = "Ross", City = "New York" },
            new Person { Name = "Chandler", City = "New York" },
            new Person { Name = "Rachel", City = "New York" }
        };

        public ViewResult Index() => View(data);
    }
}