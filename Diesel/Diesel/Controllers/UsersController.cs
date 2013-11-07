using System.Linq;

namespace Diesel.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Diesel.Core;
    using Diesel.Models;

    public class UsersController : ApiController
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public UsersController()
        {
            this._userRepository = new UserRepository();
        }


        // GET api/user
        public IEnumerable<User> Get()
        {
            return _userRepository.All;

        }

        // GET api/user/5
        public User Get(int id)
        {
            var user = _userRepository.Find(id);
            if (user == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return user;
        }

        // POST api/user
        public HttpResponseMessage Post(User value)
        {
            if (ModelState.IsValid)
            {
                _userRepository.InsertOrUpdate(value);
                _userRepository.Save();

                //Created!
                var response = Request.CreateResponse<Core.User>(HttpStatusCode.Created, value);

                ////Let them know where the new User is
                //string uri = Url.Route(null, new { id = value.Id });
                //response.Headers.Location = new Uri(Request.RequestUri, uri);

                return response;

            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        // PUT api/user/5
        public HttpResponseMessage Put(int id, User value)
        {
            if (ModelState.IsValid)
            {
                _userRepository.InsertOrUpdate(value);
                _userRepository.Save();
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        // DELETE api/user/5
        public void Delete(int id)
        {
            var result = _userRepository.Find(id);
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _userRepository.Delete(id);
        }

        public HttpResponseMessage Authenticate(User user)
        {
            Core.User result;
            try
            {
                var users = _userRepository.AllIncluding(x => x.Username == user.Username && x.Password == user.Password).ToList();
                result = users.FirstOrDefault();
                if (result == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            catch (Exception)
            {
                
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }


            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
