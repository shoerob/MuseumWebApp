using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Museum.WebApp.Models
{
    public class MainPageViewModel
    {
        public List<ExhibitViewModel> RecentExhibits { get; set; }
    }

    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
    }

    public class ArtifactViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public string CreatedAt { get; set; }
        public string ExihibitId { get; set; }
        public string Image { get; set; }
        public string ImageUrl
        {
            get
            {
                if (this.Image != null)
                {
                    return string.Format(ConfigurationManager.AppSettings["EverliveImageFormat"], this.Image);
                }
                else
                {
                    return ConfigurationManager.AppSettings["ImagePlaceHolder"];
                }
            }
        }
    }
    public class ExhibitViewModel
    {
        public string Id { get; set; }
        public string MuseumId { get; set; }        
        public string Title { get; set; }
        public string CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string Description { get; set; }
        public UserViewModel CreatedByUser { get; set; }
        public IEnumerable<ArtifactViewModel> Artifacts { get; set; }
    }
    public class MuseumViewModel
    {
        public string Id { get; set; }        
        public string Title { get; set; }
        public string CreatedAt { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
    }
}