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
        private IEverliveRestClient _everlive = new EverliveRestClient(new HttpRestClient.HttpRestClient());

        public async Task<ActionResult> Index()
        {
            try
            {
                IEnumerable<ExhibitViewModel> latestExhibits = await _getRecentExhibits();
                foreach (var exhibit in latestExhibits)
                {
                    exhibit.Artifacts = await _getExhibitArtifacts(exhibit.Id);
                    exhibit.CreatedByUser = await _getUser(exhibit.CreatedBy);
                }
                MainPageViewModel vm = new MainPageViewModel();
                vm.RecentExhibits = latestExhibits.ToList<ExhibitViewModel>();
                return View(vm);
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
                var exhibits = await _everlive.All<ExhibitViewModel>("Exhibit");
                
                //tag matching
                if (!string.IsNullOrWhiteSpace(id))
                {
                    var artifacts = await _everlive.All<ArtifactViewModel>("Artifact", string.Format("{{ \"Tag\": \"{0}\"}}", id));
                    exhibits = exhibits.Where(e => artifacts.Any(a => a.ExihibitId == e.Id));
                }

                foreach (var exhibit in exhibits)
                {
                    exhibit.Artifacts = await _getExhibitArtifacts(exhibit.Id);
                    exhibit.CreatedByUser = await _getUser(exhibit.CreatedBy);
                }
               
                return View(exhibits);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<ActionResult> Museums()
        {
            try
            {
                var museums = await _everlive.All<ExhibitViewModel>("Museum");
                
                return View(museums);
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
                var exhibit = await _everlive.GetById<ExhibitViewModel>("Exhibit", id);
                var artifacts = await _everlive.All<ArtifactViewModel>("Artifact", string.Format("{{ \"ExihibitId\": \"{0}\" }}", id));
                exhibit.Artifacts = artifacts;
                _everlive.All<ExhibitViewModel>("Exhibit", "", "{ \"CreatedAt\": 1 }", 4);
                return View(exhibit);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }


        private async Task<IEnumerable<ExhibitViewModel>> _getRecentExhibits()
        {
            try
            {

                var exhibits = await _everlive.All<ExhibitViewModel>("Exhibit", "", "{\"CreatedAt\": -1}", 5);
                return exhibits;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task<IEnumerable<ArtifactViewModel>> _getExhibitArtifacts(string exhibitId)
        {
            try
            {
                var artifacts = await _everlive.All<ArtifactViewModel>("Artifact", string.Format("{{ \"ExihibitId\": \"{0}\" }}", exhibitId));
                return artifacts;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task<UserViewModel> _getUser(string userGuid)
        {
            UserViewModel retvalue = new UserViewModel();
            retvalue.UserName = "N/A";
            try
            {
                IEnumerable<UserViewModel> users = await _everlive.All<UserViewModel>("Users", string.Format("{{ \"Id\": \"{0}\" }}", userGuid));
                if (users != null && users.Count() > 0)
                    retvalue = users.ToArray()[0];
                return retvalue;

            }
            catch (Exception) { return null; }
        }
    }
}
