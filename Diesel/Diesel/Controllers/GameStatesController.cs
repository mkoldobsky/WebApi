namespace Diesel.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Diesel.Core;
    using Diesel.Models;

    public class GameStatesController : ApiController
    {
        private readonly IGameStateRepository _gameStateRepository;
        public GameStatesController(IGameStateRepository GameStateRepository)
        {
            this._gameStateRepository = GameStateRepository;
        }

        public GameStatesController()
        {
            this._gameStateRepository = new GameStateRepository();
        }


        // GET api/GameState
        public IEnumerable<GameState> Get()
        {
            return _gameStateRepository.All;

        }

        // GET api/GameState/5
        public GameState Get(int id)
        {
            var gameState = _gameStateRepository.Find(id);
            if (gameState == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return gameState;
        }

        // POST api/GameState
        public HttpResponseMessage Post(GameState value)
        {
            if (ModelState.IsValid)
            {
                _gameStateRepository.InsertOrUpdate(value);
                _gameStateRepository.Save();

                //Created!
                var response = Request.CreateResponse<Core.GameState>(HttpStatusCode.Created, value);

                //Let them know where the new GameState is
                string uri = Url.Route(null, new { id = value.Id });
                response.Headers.Location = new Uri(Request.RequestUri, uri);

                return response;

            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        // PUT api/GameState/5
        public HttpResponseMessage Put(int id, GameState value)
        {
            if (ModelState.IsValid)
            {
                _gameStateRepository.InsertOrUpdate(value);
                _gameStateRepository.Save();
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        // DELETE api/GameState/5
        public void Delete(int id)
        {
            var result = _gameStateRepository.Find(id);
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _gameStateRepository.Delete(id);
        }
    }
}
