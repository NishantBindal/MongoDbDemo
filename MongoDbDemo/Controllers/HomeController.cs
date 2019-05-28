using System.Web.Mvc;

namespace MongoDbDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() 
        {
            //MongoDbTutorialsTopicsService mongoDbTutorialsIndexService = new MongoDbTutorialsTopicsService(new MongoDbConfigProvider());
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}