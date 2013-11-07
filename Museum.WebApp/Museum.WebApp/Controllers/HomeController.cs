using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EverLive;
using Museum.WebApp.Models;

namespace Museum.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private IEverliveRestClient _everlive;
        public async Task<ActionResult> Index()
        {
            try
            {
                return await Exhibits();                
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Exhibits(string id = "")
        {
            try
            {
                _everlive = new EverliveRestClient(new HttpRestClient.HttpRestClient());                
                var exhibits = await _everlive.All<ExhibitViewModel>("Exhibit");
                if (!string.IsNullOrWhiteSpace(id))
                {
                    var artifacts = await _everlive.All<ArtifactViewModel>("Artifact", string.Format("{{ \"Tag\": \"{0}\"}}", id));
                    exhibits = exhibits.Where(e => artifacts.Any(a => a.ExihibitId == e.Id));
                }
               
                return View(exhibits);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
     
        //exhibit id
        public async Task<ActionResult> Gallery(string id)
        {
            try
            {
                _everlive = new EverliveRestClient(new HttpRestClient.HttpRestClient());
                var exhibit = await _everlive.GetById<ExhibitViewModel>("Exhibit", id);
                var artifacts = await _everlive.All<ArtifactViewModel>("Artifact", string.Format("{{ \"ExihibitId\": \"{0}\" }}", id));
                exhibit.Artifacts = artifacts;

                return View(exhibit);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
