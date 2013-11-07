namespace Diesel.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Diesel.Core;
    using Diesel.Models;

    public class ChallengeMatchesController : ApiController
    {
        private readonly IChallengeMatchRepository _challengeMatchRepository;
        public ChallengeMatchesController(IChallengeMatchRepository challengeMatchRepository)
        {
            this._challengeMatchRepository = challengeMatchRepository;
        }

        public ChallengeMatchesController()
        {
            this._challengeMatchRepository = new ChallengeMatchRepository();
        }


        // GET api/ChallengeMatche
        public IEnumerable<ChallengeMatch> Get()
        {
            return _challengeMatchRepository.All;

        }

        // GET api/ChallengeMatche/5
        public ChallengeMatch Get(int id)
        {
            var challengeMatch = _challengeMatchRepository.Find(id);
            if (challengeMatch == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return challengeMatch;
        }

        // POST api/ChallengeMatche
        public HttpResponseMessage Post(ChallengeMatch value)
        {
            if (ModelState.IsValid)
            {
                _challengeMatchRepository.InsertOrUpdate(value);
                _challengeMatchRepository.Save();

                //Created!
                var response = Request.CreateResponse<Core.ChallengeMatch>(HttpStatusCode.Created, value);

                //Let them know where the new ChallengeMatche is
                string uri = Url.Route(null, new { id = value.Id });
                response.Headers.Location = new Uri(Request.RequestUri, uri);

                return response;

            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        // PUT api/ChallengeMatche/5
        public HttpResponseMessage Put(int id, ChallengeMatch value)
        {
            if (ModelState.IsValid)
            {
                _challengeMatchRepository.InsertOrUpdate(value);
                _challengeMatchRepository.Save();
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        // DELETE api/ChallengeMatche/5
        public void Delete(int id)
        {
            var result = _challengeMatchRepository.Find(id);
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _challengeMatchRepository.Delete(id);
        }
    }
}
