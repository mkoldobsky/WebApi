namespace Diesel.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Diesel.Core;
    using Diesel.Models;

    public class BoardStatesController : ApiController
    {
        private readonly IBoardStateRepository _boardStateRepository;
        public BoardStatesController(IBoardStateRepository boardStateRepository)
        {
            this._boardStateRepository = boardStateRepository;
        }

        public BoardStatesController()
        {
            this._boardStateRepository = new BoardStateRepository();
        }


        // GET api/BoardState
        public IEnumerable<BoardState> Get()
        {
            return _boardStateRepository.All;

        }

        // GET api/BoardState/5
        public BoardState Get(int id)
        {
            var boardState = _boardStateRepository.Find(id);
            if (boardState == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return boardState;
        }

        // POST api/BoardState
        public HttpResponseMessage Post(BoardState value)
        {
            if (ModelState.IsValid)
            {
                _boardStateRepository.InsertOrUpdate(value);
                _boardStateRepository.Save();

                //Created!
                var response = Request.CreateResponse<Core.BoardState>(HttpStatusCode.Created, value);

                //Let them know where the new BoardState is
                string uri = Url.Route(null, new { id = value.Id });
                response.Headers.Location = new Uri(Request.RequestUri, uri);

                return response;

            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        // PUT api/BoardState/5
        public HttpResponseMessage Put(int id, BoardState value)
        {
            if (ModelState.IsValid)
            {
                _boardStateRepository.InsertOrUpdate(value);
                _boardStateRepository.Save();
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        // DELETE api/BoardState/5
        public void Delete(int id)
        {
            var result = _boardStateRepository.Find(id);
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _boardStateRepository.Delete(id);
        }
    }
}
