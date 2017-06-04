using Microsoft.AspNetCore.Mvc;
using UrlsAndRoutes.Models;

namespace UrlsAndRoutes.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index () => View("Result", new Result {
            Controller = nameof(HomeController),
            Action = nameof(Index)
        });

        public ViewResult CustomVariables() {
            Result r = new Result {
                Controller = nameof(HomeController),
                Action = nameof(CustomVariables)
            };
            r.Data["id"] = RouteData.Values["id"];
            return View("Result", r);
        }
        //The second method have the same name of the custom variables
        //in the startup clase routes so the parametrer is asigned 
        public ViewResult CustomVariable(string id) {
            Result r = new Result {
                Controller = nameof(HomeController),
                Action = nameof(CustomVariables)
            };
            r.Data["id"] = id ?? "<no value>";
            r.Data["catchall"] = RouteData.Values["catchall"];
            r.Data["url"] = Url.Action("CustomVariable", "Home", new { id = 100 });
            return View("Result", r);
        }
    }
}