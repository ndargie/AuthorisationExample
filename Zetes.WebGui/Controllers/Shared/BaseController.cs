using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Zetes.WebGui.Controllers.Shared
{
    public class BaseController : Controller
    {
        protected async Task<string> GetAccessToken()
        {
            return  await HttpContext.GetTokenAsync("access_token");
        }

        protected string GetUsername()
        {
            return HttpContext.User.Identity.Name;
        }
    }
}
