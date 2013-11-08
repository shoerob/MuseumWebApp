using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Museum.WebApp.Models
{
    public class ArtifactViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public string CreatedAt { get; set; }
        public string ExihibitId { get; set; }
        public string Image { get; set; }
    }
    public class ExhibitViewModel
    {
        public string Id { get; set; }
        public string MuseumId { get; set; }        
        public string Title { get; set; }
        public string CreatedAt { get; set; }
        public string Description { get; set; }
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