using Microsoft.AspNetCore.Mvc;
using UrlsAndRoutes.Models;

namespace UrlsAndRoutes.Controllers
{
    [Route("app/[controller]/actions/[action]/{id:weekday?}")]
    public class CustomerController : Controller
    {
        public ViewResult Index() => View("Result", new Result{
            Controller = nameof(CustomerController),
            Action = nameof(Index)
        });

        public ViewResult List() => View("Result", 
            new Result {
                Controller = nameof(CustomerController),
                Action = nameof(List)
            });
    }
}