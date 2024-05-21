using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;

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
        public JsonResult Create(Findling findling)
        {
            var fx = _context.Findlings.Add(findling);
            findling.Id = fx.Entity.Id;
            _context.SaveChanges();

            return new JsonResult(Ok(findling));            
        }

        // Get
        [HttpGet("{id:int}")]
        public JsonResult Get(int id)
        {
            var findling = _context.Findlings.Find(id);
            if(findling == null) return new JsonResult(NotFound());
            
            return new JsonResult(Ok(findling));
        }

        // Put
        [HttpPut]
        public JsonResult Update(Findling findling)
        {
            var findlingInDb = _context.Findlings.Find(findling.Id);
            if (findlingInDb != null)
            {
                findlingInDb = findling;
                _context.SaveChanges();

                return new JsonResult(Ok(findling));
            }
            else 
            {
                return new JsonResult(NotFound(findling));
            }
        }

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