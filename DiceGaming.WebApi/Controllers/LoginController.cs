using DiceGaming.Core.Exceptions;
using DiceGaming.Core.Helpers;
using DiceGaming.Core.Models;
using DiceGaming.Core.Repositories;
using DiceGaming.WebApi.Requests;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace DiceGaming.WebApi.Controllers
{

    [RoutePrefix("api/logins")]
    public class LoginController : ApiController
    {
        private readonly ILoginRepository loginRepository;
        private readonly IUserRepository userRepository;

        public LoginController(ILoginRepository loginRepository, IUserRepository userRepository)
        {
            this.loginRepository = loginRepository;
            this.userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("")]
        public Task<HttpResponseMessage> Login([FromBody] LoginRequest loginRequest)
        {
            var user = userRepository.Get(loginRequest.Username, loginRequest.Password);

            string token = TokenManager.GenerateToken();

            var login = new LoginDto()
            {
                UserId = user.Id,
                Token = token
            };

            login = loginRepository.LoginUser(login);

            return Task.FromResult(Request.CreateResponse(HttpStatusCode.Created, login));
        }

        [HttpDelete]
        [Route("{id}")]
        public Task<HttpResponseMessage> Logout(int id)
        {
            string token = Request.Headers.GetValues("DiceGaming-Token").FirstOrDefault();

            if (!loginRepository.HasLoginAndToken(id, token))
                throw new NotFoundException();

            loginRepository.Delete(id);

            return Task.FromResult(Request.CreateResponse(HttpStatusCode.NoContent));
        }
    }
}
