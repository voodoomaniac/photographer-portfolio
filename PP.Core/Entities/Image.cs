using System;
using System.ComponentModel.DataAnnotations;

namespace PP.Core.Entities
{
    public class Image : BaseEntity
    {
        public string Path { get; set; }

        [StringLength(50), Required]
        public string Description { get; set; }

        public DateTime UploadDate { get; set; }
    }
}