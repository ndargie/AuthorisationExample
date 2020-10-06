using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Zetes.WebGui.Controllers.Shared;

namespace Zetes.WebGui.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
