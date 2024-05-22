using System;
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
    public class TestDataController : ControllerBase
    {
        private readonly ApiContext _context;

        public TestDataController(ApiContext apiContext)
        {
            _context = apiContext;
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

        // Hydrate
        [HttpGet("TestFindlingModelValidation")]
        public JsonResult TestFindlingModelValidation()
        {
            bool hydrated = _context.Hydrate();

            if(_context.Findlings.All(f =>
                    !string.IsNullOrEmpty(f.Guid) && 
                    f.Id > 0 &&
                    f.UserId > 0 &&
                    !string.IsNullOrEmpty(f.UserName) &&
                    !string.IsNullOrEmpty(f.Url) && 
                    !string.IsNullOrEmpty(f.Title) &&
                    !string.IsNullOrEmpty(f.Image) && 
                    !string.IsNullOrEmpty(f.Timestamp.ToString()) &&
                    f.Timestamp > DateTime.MinValue && f.Timestamp < DateTime.MaxValue
                    ))
            {
                return new JsonResult(Ok());
            }
            else
            {
                return new JsonResult(ValidationProblem());
            }
        }
    }
}