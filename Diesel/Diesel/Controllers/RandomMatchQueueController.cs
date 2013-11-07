namespace Diesel.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Diesel.Core;
    using Diesel.Models;

    public class RandomMatchQueueController : ApiController
    {
        private readonly IRandomMatchQueueRepository _randomMatchQueueRepository;
        public RandomMatchQueueController(IRandomMatchQueueRepository randomMatchQueueRepository)
        {
            this._randomMatchQueueRepository = randomMatchQueueRepository;
        }

        public RandomMatchQueueController()
        {
            this._randomMatchQueueRepository = new RandomMatchQueueRepository();
        }


        // GET api/RandomMatchQueue
        public IEnumerable<RandomMatchQueue> Get()
        {
            return _randomMatchQueueRepository.All;

        }

        // GET api/RandomMatchQueue/5
        public RandomMatchQueue Get(int id)
        {
            var randomMatchQueue = _randomMatchQueueRepository.Find(id);
            if (randomMatchQueue == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return randomMatchQueue;
        }

        // POST api/RandomMatchQueue
        public HttpResponseMessage Post(RandomMatchQueue value)
        {
            if (ModelState.IsValid)
            {
                _randomMatchQueueRepository.InsertOrUpdate(value);
                _randomMatchQueueRepository.Save();

                //Created!
                var response = Request.CreateResponse<Core.RandomMatchQueue>(HttpStatusCode.Created, value);

                //Let them know where the new RandomMatchQueue is
                string uri = Url.Route(null, new { id = value.Id });
                response.Headers.Location = new Uri(Request.RequestUri, uri);

                return response;

            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        // PUT api/RandomMatchQueue/5
        public HttpResponseMessage Put(int id, RandomMatchQueue value)
        {
            if (ModelState.IsValid)
            {
                _randomMatchQueueRepository.InsertOrUpdate(value);
                _randomMatchQueueRepository.Save();
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        // DELETE api/RandomMatchQueue/5
        public void Delete(int id)
        {
            var result = _randomMatchQueueRepository.Find(id);
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _randomMatchQueueRepository.Delete(id);
        }
    }
}
