using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API.Controllers
{
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

        // Delete
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var findlingToDelete = _context.Findlings.Find(id);

            if(findlingToDelete == null) return new JsonResult(NotFound());

            var res = _context.Findlings.Remove(findlingToDelete);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }

        // List
        [HttpGet("List")]
        public JsonResult List()
        {
            var findlings = _context.Findlings.ToList();

            return new JsonResult(Ok(findlings));
        }

        // Hydrate
        [HttpGet("Hydrate")]
        public JsonResult Hydrate()
        {
            bool hydrated = _context.Hydrate();

            if(hydrated)
            {
                return new JsonResult(Ok());
            }
            else
            {
                return new JsonResult(NotFound());
            }
        }
    }
}