namespace Diesel.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Diesel.Core;
    using Diesel.Models;

    public class ActiveGamesController : ApiController
    {
        private readonly IActiveGameRepository _activeGameRepository;
        public ActiveGamesController(IActiveGameRepository ActiveGameRepository)
        {
            this._activeGameRepository = ActiveGameRepository;
        }

        public ActiveGamesController()
        {
            this._activeGameRepository = new ActiveGameRepository();
        }


        // GET api/ActiveGame
        public IEnumerable<ActiveGame> Get()
        {
            return _activeGameRepository.All;

        }

        // GET api/ActiveGame/5
        public ActiveGame Get(int id)
        {
            var activeGame = _activeGameRepository.Find(id);
            if (activeGame == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return activeGame;
        }

        // POST api/ActiveGame
        public HttpResponseMessage Post(ActiveGame value)
        {
            if (ModelState.IsValid)
            {
                _activeGameRepository.InsertOrUpdate(value);
                _activeGameRepository.Save();

                //Created!
                var response = Request.CreateResponse<Core.ActiveGame>(HttpStatusCode.Created, value);

                //Let them know where the new ActiveGame is
                string uri = Url.Route(null, new { id = value.Id });
                response.Headers.Location = new Uri(Request.RequestUri, uri);

                return response;

            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        // PUT api/ActiveGame/5
        public HttpResponseMessage Put(int id, ActiveGame value)
        {
            if (ModelState.IsValid)
            {
                _activeGameRepository.InsertOrUpdate(value);
                _activeGameRepository.Save();
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        // DELETE api/ActiveGame/5
        public void Delete(int id)
        {
            var result = _activeGameRepository.Find(id);
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _activeGameRepository.Delete(id);
        }
    }
}
