using System.Collections.Generic;

namespace PP.Core.Entities
{
    public class PagedList<T>
    {
        public List<T> ItemsCollection { get; set; }

        public int CurrentPage { get; set; }

        public int ItemsPerPage { get; set; }

        public int TotalRecords { get; set; }

        public int TotalPages { get; set; }

        public bool HasNextPage { get; set; }

        public bool HasPreviousPage { get; set; }

        public PagedList()
        {
            this.ItemsCollection = new List<T>();
        }
    }
}