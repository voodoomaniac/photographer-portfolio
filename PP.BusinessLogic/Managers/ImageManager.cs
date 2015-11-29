using System;
using System.Drawing;
using System.IO;
using System.Linq;
using PP.Core.Entities;
using PP.Core.Interfaces.Repository;
using Image = PP.Core.Entities.Image;
using System.Web;

namespace PP.BusinessLogic.Managers
{
    public class ImageManager
    {
        private readonly IRepository<Image> _imageRepository;

        public ImageManager(IRepository<Image> imageRepository)
        {
            this._imageRepository = imageRepository;
        }

        public void Add(Image image, HttpPostedFileBase file, string serverPath)
        {
            var fileName = Guid.NewGuid().ToString();
            var extension = System.IO.Path.GetExtension(file.FileName).ToLower();
            image.Path = AppConfiguration.ImagePath + fileName + extension;
            image.UploadDate = DateTime.Now;

            using (var img = System.Drawing.Image.FromStream(file.InputStream))
            {
                using (var newImg = new Bitmap(img))
                {
                    newImg.Save(serverPath + image.Path, img.RawFormat);
                }
            }

            this._imageRepository.Add(image);
        }

        public Image Get(int imageId)
        {
            return this._imageRepository.Get(imageId);
        }

        public void Remove(int imageId, string serverPath)
        {
            var imageToRemove = this.Get(imageId);
            File.Delete(serverPath + imageToRemove.Path);
            this._imageRepository.Remove(imageToRemove);
        }

        public PagedList<Image> GetPage(int itemsPerPage, int currentPage)
        {

            var itemsCollection = this._imageRepository.GetPage(itemsPerPage, currentPage, i => i.UploadDate).ToList();
            var totalRecords = this._imageRepository.GetAll().Count();
            var totalPages = (totalRecords + itemsPerPage - 1) / itemsPerPage;
            var hasNextPage = currentPage < totalPages;
            var hasPreviousPage = currentPage > 1;

            var pagedList = new PagedList<Image>
            {
                ItemsCollection = itemsCollection,
                CurrentPage = currentPage,
                ItemsPerPage = itemsPerPage,
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                HasNextPage = hasNextPage,
                HasPreviousPage = hasPreviousPage
            };

            return pagedList;
        }
    }
}