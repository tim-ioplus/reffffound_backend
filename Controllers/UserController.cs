using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
using System.Reflection.Metadata.Ecma335;

namespace API.Controllers
{
    //[Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApiDataContext _context;

        public UserController(ApiDataContext apiContext)
        {
            _context = apiContext;
        }

        // C reate
        [HttpPost("Create")]
        public JsonResult Create(User user)
        {
            if(user.Id == 0)
            {
                var usx = _context.Users.Add(user);
                if(usx.Entity.Id != 0)
                {
                    return new JsonResult(Ok(usx.Entity));
                }
                else
                {
                    return new JsonResult(NotFound());
                }
            }
            else
            {
                return new JsonResult(NotFound());
            }
        }

        // R ead
        [HttpGet("Read")]
        public JsonResult Read(User user)
        {
            User? foundUser = null;
            // @todo Authenticate, Authorize
            if(user.Id > 0)
            {
                var usx = _context.Users.SingleOrDefault(u => u.Id == user.Id);
                foundUser = usx;                
            }
            else if(!string.IsNullOrWhiteSpace(user.Name))
            {
                var usx = _context.Users.SingleOrDefault(u => u.Name.Equals(user.Name));
                foundUser = usx;
            }
            else if(!string.IsNullOrWhiteSpace(user.EMail))
            {
                var usx = _context.Users.SingleOrDefault(u => u.EMail.Equals(user.EMail));
                foundUser = usx;
            }
            

            if(foundUser != null)
            {
                return new JsonResult(Ok(foundUser));
            }
            else
            {
                return new JsonResult(NotFound());
            }
        }

        // U pdate
        [HttpPut("Update")]
        public JsonResult Update(User user)
        {
            if(user.Id > 0)
            {
                var userFromDb = _context.Users.SingleOrDefault(u => u.Id == user.Id);
                if(userFromDb == null) return new JsonResult(NotFound());

                userFromDb = user;
                _context.SaveChanges();
                
                return new JsonResult(Ok()); 
            }

            return new JsonResult(BadRequest());
        }

        // D elete
        [HttpDelete("Delete")]
        public JsonResult Delete(User user)
        {
            var usertoDelete = _context.Users.Find(user.Id);
            if(usertoDelete == null) return new JsonResult(NotFound());

            var result = _context.Users.Remove(user);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }
        // L ist
        [HttpGet("List")]
        public JsonResult List()
        {
            var users = _context.Users.ToList();

            if(users.Count == 0)
            {
                return new JsonResult(NotFound());
            }
            else
            {
                return new JsonResult(Ok(users));
            }
        }
    }
}