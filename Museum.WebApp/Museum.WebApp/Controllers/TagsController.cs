using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using EverLive;
using Museum.WebApp.Models;

namespace Museum.WebApp.Controllers
{
    public class TagsController : ApiController
    {
        private IEverliveRestClient _everlive;
        // GET api/values
        public async Task<IEnumerable<string>> Get()
        {
            _everlive = new EverliveRestClient(new HttpRestClient.HttpRestClient());
            var tags = await _everlive.All<ArtifactViewModel>("Artifact", fields: "{ \"Tag\": 1 }");
            return tags.Select(e => e.Tag).Distinct();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}