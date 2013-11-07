namespace Diesel.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Diesel.Core;
    using Diesel.Models;

    public class CommandListController : ApiController
    {
        private readonly ICommandListRepository _commandListRepository;
        public CommandListController(ICommandListRepository commandListRepository)
        {
            this._commandListRepository = commandListRepository;
        }

        public CommandListController()
        {
            this._commandListRepository = new CommandListRepository();
        }


        // GET api/CommandList
        public IEnumerable<CommandList> Get()
        {
            return _commandListRepository.All;

        }

        // GET api/CommandList/5
        public CommandList Get(int id)
        {
            var commandList = _commandListRepository.Find(id);
            if (commandList == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return commandList;
        }

        // POST api/CommandList
        public HttpResponseMessage Post(CommandList value)
        {
            if (ModelState.IsValid)
            {
                _commandListRepository.InsertOrUpdate(value);
                _commandListRepository.Save();

                //Created!
                var response = Request.CreateResponse<Core.CommandList>(HttpStatusCode.Created, value);

                //Let them know where the new CommandList is
                string uri = Url.Route(null, new { id = value.Id });
                response.Headers.Location = new Uri(Request.RequestUri, uri);

                return response;

            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        // PUT api/CommandList/5
        public HttpResponseMessage Put(int id, CommandList value)
        {
            if (ModelState.IsValid)
            {
                _commandListRepository.InsertOrUpdate(value);
                _commandListRepository.Save();
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        // DELETE api/CommandList/5
        public void Delete(int id)
        {
            var result = _commandListRepository.Find(id);
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _commandListRepository.Delete(id);
        }
    }
}
