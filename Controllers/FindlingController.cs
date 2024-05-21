using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    //@todo [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FindlingController : ControllerBase
    {
        private readonly ApiContext _context;

        public FindlingController(ApiContext apiContext)
        {
            _context = apiContext;
        }

        // Create/Edit
        [HttpPost]
        public JsonResult CreateEdit(Findling findling)
        {
            var idToCheck = findling.Id;

            if(findling.Id == 0)
            {
                var fx = _context.Findlings.Add(findling);
                findling.Id = fx.Entity.Id;
                
                _context.SaveChanges();
            }
            else
            {
                var findlingDb = _context.Findlings.SingleOrDefault(b => b.Id == findling.Id);
                if(findlingDb == null) return new JsonResult(NotFound());

                findlingDb = findling;

                _context.SaveChanges();

                var findlingtoCheck = _context.Findlings.SingleOrDefault(b => b.Id == findling.Id);
                if(findlingtoCheck!=null && (findlingtoCheck.Id != idToCheck))
                {                
                    return new JsonResult(Conflict(findlingtoCheck));
                }
            }

            return new JsonResult(Ok(findling));
        }

        [HttpPost]
        public JsonResult Create(Findlng findling)
        {
            var fx = _context.Findlings.Add(findling);
            findling.Id = fx.Entity.Id;
            _context.SaveChanges();

            return new JsonResult(Ok(findling));            
        }

        [HttpGet("{id:int}")]
        public JsonResult Get(int id)
        {
            var findling = _context.Findlings.Find(id);
            if(findling == null) return new JsonResult(NotFound());
            
            return new JsonResult(Ok(findling));
        }

        [HttpUpdate]
        

        // Delete
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var findlingToDelete = _context.Findlings.Find(id);
            if(findlingToDelete == null) return new JsonResult(NotFound());

            var result = _context.Findlings.Remove(findlingToDelete);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }

        // List
        [HttpGet("List")]
        public JsonResult List()
        {
            var findlings = _context.Findlings.ToList();

            if(findlings.Count == 0)
            {
                return new JsonResult(NotFound());
            }
            else
            {
                return new JsonResult(Ok(findlings));
            }
        }        
    }
}