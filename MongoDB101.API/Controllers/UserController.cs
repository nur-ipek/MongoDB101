using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB101.API.Model;
using MongoDB101.API.Service;
using MongoDB101.API.Settings;
using System.Collections.Generic;

namespace MongoDB101.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserService _userService;

        public UserController(IDbSettings dbSettings)
        {
            _userService = new UserService(dbSettings);
        }

        [HttpGet]
        public ActionResult<List<User>> GetAll() => _userService.GetAll();

        [HttpGet("{id:length(24)}")]
        public ActionResult<User> GetSingle(string id) => _userService.GetSingle(id);

        [HttpPost]
        public ActionResult<User> Add(User user) => _userService.Add(user);

        [HttpPut] //(Kullanıcıyı güncelleme)
        public ActionResult Update(User user)
        { 
            var isUser = _userService.GetSingle(user.Id);

            if (user == null)
                return NotFound();
            _userService.Update(user);

            return Ok();
        }

        [HttpDelete("{id:length(24)}")]
        public ActionResult Delete(string id)
        {
            var isUser = _userService.GetSingle(id);

            if (User == null)
                return NotFound();
            _userService.Delete(id);

            return Ok();
        }
    }
}
