using System.Web.Mvc;

namespace Lottery.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

#if DEBUG
            return Redirect("/swagger/ui/index#/");
#else
          var env = ConfigHelper.Value("env");
            if (env == "dev")
            {
                return Redirect("dev/swagger/ui/index#/");
            }
            else if (env == "prod")
            {
                return Redirect("prod/swagger/ui/index#/");
            }else if (env == "test")
            {
                return Redirect("test/swagger/ui/index#/");
            }
            return Redirect("/swagger/ui/index#/");
#endif
        }
    }
}