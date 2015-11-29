using System.Configuration;

namespace PP.BusinessLogic
{
    public static class AppConfiguration
    {
        public static int ItemsPerPage
        {
            get
            {
                int itemsPerPage;
                int.TryParse(ConfigurationManager.AppSettings["ItemsPerPage"], out itemsPerPage);
                return itemsPerPage;
            }
        }

        public static int DefaultPageNumber
        {
            get
            {
                int defaultPageNumber;
                int.TryParse(ConfigurationManager.AppSettings["DefaultPageNumber"], out defaultPageNumber);
                return defaultPageNumber;
            }
        }


        public static string ImagePath
        {
            get
            {
                return ConfigurationManager.AppSettings["ImagePath"];
            }
        }

        public static string ArchivePath
        {
            get
            {
                return ConfigurationManager.AppSettings["ArchivePath"];
            }
        }
    }
}