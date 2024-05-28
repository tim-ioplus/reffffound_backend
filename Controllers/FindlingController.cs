using System.Xml.Linq;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //@todo [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FindlingController : ControllerBase
    {
        private readonly ApiDataContext _context;

        public FindlingController(ApiDataContext apiContext)
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
            findling.Guid = Guid.NewGuid().ToString().Replace("-","");
            findling.Furl = "/image/" + findling.Guid;

            var fx = _context.Findlings.Add(findling);
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
            if (_context.Findlings.Any(f => f.Id ==findling.Id))
            {
                _context.Findlings.Update(findling);
                _context.SaveChanges();
            }
            else 
            {
                return new JsonResult(NotFound(findling));
            }


            return new JsonResult(Ok(findling));
        }

        // Delete
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var findlingToDelete = _context.Findlings.Find(id);
            if(findlingToDelete == null) return new JsonResult(NotFound());

            var result = _context.Findlings.Remove(findlingToDelete);
            if(result.State != Microsoft.EntityFrameworkCore.EntityState.Deleted)
            {
                return new JsonResult(Conflict());
            }

            _context.SaveChanges();

            return new JsonResult(Ok());
        }

        // List
        [HttpGet("{page:int}")]
        public JsonResult List(int page)
        {
            this.HttpContext.Response.Headers.AccessControlAllowOrigin = "http://localhost:4200";
            
            int skipCount = (page-1)*10;
            int takeCount = 10;

            var findlings = _context.Findlings.Skip(skipCount).Take(takeCount).OrderBy(f => f.Id).ToList();

            if(findlings.Count == 0)
            {
                return new JsonResult(NotFound());
            }
            else
            {
                return new JsonResult(Ok(findlings));
            }
        }

        // List
        [HttpGet("ListAll")]
        public JsonResult List()
        {
            var findlings = _context.Findlings.OrderBy(f => f.Id).ToList();;

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