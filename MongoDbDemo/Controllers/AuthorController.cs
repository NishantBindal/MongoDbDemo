using ExpressMapper;
using Projection = MongoDbDemo.Models;
using MongoDbDemo.Providers;
using MongoDbDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Dto = MongoDbDemo.Models.Dtos;
using MongoDbDemo.Models.Dtos;
using System.IO;

namespace MongoDbDemo.Controllers
{
    public class AuthorController : Controller
    {
        private MongoDbTutorialsTopicsService _mongoDbTutorialsTopicsService;
        public AuthorController()
        {
            var dbConfigProvider = new MongoDbConfigProvider();
            _mongoDbTutorialsTopicsService = new MongoDbTutorialsTopicsService(dbConfigProvider);
        }
        // GET: Author
        public ActionResult Index()
        {
            return View();
        }

        // GET: Author/Details/5
        public ActionResult Details(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("FecthAll","Topic");
            }
            var authorStats = _mongoDbTutorialsTopicsService.GetAllByAuthorName(name);
            return View(Mapper.Map<IEnumerable<Projection.Topic>, IEnumerable<Dto.Topic>>(authorStats));
        }

        // GET: Author/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Author/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Author/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Author/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Author/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
