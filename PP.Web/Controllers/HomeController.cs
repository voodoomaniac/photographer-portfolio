using System.IO;
using System.IO.Compression;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PP.BusinessLogic;
using PP.BusinessLogic.Managers;
using PP.Core.Entities;

namespace PP.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ImageManager _imageManager;

        public HomeController(ImageManager imageManager)
        {
            this._imageManager = imageManager;
        }

        public ActionResult Index(int? itemsPerPage = null, int? currentPage = null)
        {
            var images = this._imageManager.GetPage(itemsPerPage ?? AppConfiguration.ItemsPerPage,
                currentPage ?? AppConfiguration.DefaultPageNumber);

            return View(images);
        }

        public ActionResult DownloadArchivedPhotos()
        {
            var zipFilePath = Server.MapPath("~") + AppConfiguration.ArchivePath + "gallery.zip";

            if (System.IO.File.Exists(zipFilePath))
            {
                System.IO.File.Delete(zipFilePath);
            }

            ZipFile.CreateFromDirectory(Server.MapPath("~") + AppConfiguration.ImagePath, zipFilePath);

            return File(zipFilePath, "application/zip", "photogallery.zip");
        }

        public ActionResult SinglePhoto(int id = 0)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad Request");
            }
            var image = this._imageManager.Get(id);
            return View(image);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Upload()
        {
            var image = new Image();
            return View(image);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Upload(Image image, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
                return View(image);

            if (file == null || file.ContentLength == 0)
            {
                ViewBag.error = "Please choose a file";
                return View(image);
            }

            this._imageManager.Add(image, file, Server.MapPath("~"));

            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Remove(int id = 0)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad Request");
            }
            this._imageManager.Remove(id, Server.MapPath("~"));
            return RedirectToAction("Index");
        }
    }
}