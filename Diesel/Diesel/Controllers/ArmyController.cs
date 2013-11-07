namespace Diesel.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Diesel.Core;
    using Diesel.Models;

    public class ArmyController : ApiController
    {
        private readonly IArmyRepository _armyRepository;
        public ArmyController(IArmyRepository armyRepository)
        {
            this._armyRepository = armyRepository;
        }

        public ArmyController()
        {
            this._armyRepository = new ArmyRepository();
        }


        // GET api/Armies
        public IEnumerable<Army> Get()
        {
            return _armyRepository.All;

        }

        // GET api/Armies/5
        public Army Get(int id)
        {
            var army = _armyRepository.Find(id);
            if (army == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return army;
        }

        // POST api/Armies
        public HttpResponseMessage Post(Army value)
        {
            if (ModelState.IsValid)
            {
                _armyRepository.InsertOrUpdate(value);
                _armyRepository.Save();

                //Created!
                var response = Request.CreateResponse<Core.Army>(HttpStatusCode.Created, value);

                //Let them know where the new Armies is
                string uri = Url.Route(null, new { id = value.Id });
                response.Headers.Location = new Uri(Request.RequestUri, uri);

                return response;

            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        // PUT api/Armies/5
        public HttpResponseMessage Put(int id, Army value)
        {
            if (ModelState.IsValid)
            {
                _armyRepository.InsertOrUpdate(value);
                _armyRepository.Save();
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        // DELETE api/Armies/5
        public void Delete(int id)
        {
            var result = _armyRepository.Find(id);
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _armyRepository.Delete(id);
        }
    }
}
