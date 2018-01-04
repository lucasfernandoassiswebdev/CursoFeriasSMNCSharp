using System.Web.Mvc;

namespace ProjetoCursoFeriasSMN.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home";
            return View();
        }
    }
}