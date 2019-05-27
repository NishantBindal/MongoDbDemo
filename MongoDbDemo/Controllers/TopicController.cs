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
    public class TopicController : Controller
    {
        private MongoDbTutorialsTopicsService _mongoDbTutorialsTopicsService;
        public TopicController()
        {
            var dbConfigProvider = new MongoDbConfigProvider();
            _mongoDbTutorialsTopicsService = new MongoDbTutorialsTopicsService(dbConfigProvider);
        }
        // GET: Topic
        public async Task<ActionResult> FetchAll()
        {

            var topics = await _mongoDbTutorialsTopicsService.GetAllAsync();
            var authorStats = _mongoDbTutorialsTopicsService.GetTotalLikesPerAuthor().ToList();
            return View(new TopicsData()
            {
                Topics = Mapper.Map<IEnumerable<Projection.Topic>, IEnumerable<Dto.Topic>>(topics),
                AuthorStats = Mapper.Map<IEnumerable<Projection.AuthorStats>, IEnumerable<Dto.AuthorStats>>(authorStats)
            });
        }
        // GET: Topic/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("FecthAll");
            }
            var doc = await _mongoDbTutorialsTopicsService.GetByIdAsync(id);
            return View(Mapper.Map<Projection.Topic, Dto.Topic>(doc));
        }

        // GET: Topic/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Topic/Create
        [HttpPost]
        public async Task<ActionResult> Create(Dto.Topic document)
        {
            if (document == null)
            {
                return View();
            }
            if (!ModelState.IsValid)
            {
                return View(document);
            }
            try
            {
                // TODO: Add insert logic here
                var documentProjection = Mapper.Map<Dto.Topic, Projection.Topic>(document);
                documentProjection.File = CovertFileToString(document.FileBase);
                var response = await _mongoDbTutorialsTopicsService.CreateAsync(documentProjection);
                if (response)
                    return RedirectToAction("FetchAll");
                else
                    return RedirectToAction("Create");
            }
            catch
            {
                return View();
            }
        }

        // GET: Topic/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("FecthAll");
            }
            var doc = await _mongoDbTutorialsTopicsService.GetByIdAsync(id);
            return View(Mapper.Map<Projection.Topic, Dto.Topic>(doc));
        }

        // POST: Topic/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(string id, Dto.Topic document)
        {
            if (string.IsNullOrEmpty(id) || document == null)
            {
                return RedirectToAction("FecthAll");
            }
            var response = await _mongoDbTutorialsTopicsService.UpdateAsync(id, Mapper.Map<Dto.Topic, Projection.Topic>(document));
            if (response)
                return RedirectToAction("FetchAll");
            else
                return View();
        }

        // GET: Topic/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("FecthAll");
            }
            var doc = await _mongoDbTutorialsTopicsService.GetByIdAsync(id);
            return View(Mapper.Map<Projection.Topic, Dto.Topic>(doc));
        }

        // POST: Topic/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(string id, FormCollection collection)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("FecthAll");
            }
            var response = await _mongoDbTutorialsTopicsService.DeleteAsync(id);
            if (response)
                return RedirectToAction("FetchAll");
            else
                return View();
        }
        public async Task<ActionResult> Like(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("FecthAll");
            }
            var result = await _mongoDbTutorialsTopicsService.LikeAsync(id);
            return RedirectToAction("FetchAll");
        }
        private string CovertFileToString(HttpPostedFileBase httpPostedFileBase)
        {
            if (httpPostedFileBase.ContentLength > 0)
            {
                //get the file's name
                string theFileName = Path.GetFileName(httpPostedFileBase.FileName);

                //get the bytes from the content stream of the file
                byte[] thePictureAsBytes = new byte[httpPostedFileBase.ContentLength];
                using (BinaryReader theReader = new BinaryReader(httpPostedFileBase.InputStream))
                {
                    thePictureAsBytes = theReader.ReadBytes(httpPostedFileBase.ContentLength);
                }

                //convert the bytes of image data to a string using the Base64 encoding
                string thePictureDataAsString = Convert.ToBase64String(thePictureAsBytes);
                return thePictureDataAsString;
            }
            return string.Empty;
        }
    }
}
