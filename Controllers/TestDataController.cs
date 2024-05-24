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
        private readonly ApiDataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TestDataController(ApiDataContext apiContext, IWebHostEnvironment webHostEnvironment)
        {
            _context = apiContext;
            _webHostEnvironment = webHostEnvironment;
            _context.ContentRootPath = _webHostEnvironment.ContentRootPath;
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

            if(_context.Findlings.ToList().All(f =>
                    !string.IsNullOrEmpty(f.Guid) && 
                    f.Id > 0 &&
                    //f.UserId > 0 &&
                    //!string.IsNullOrEmpty(f.UserName) &&
                    !string.IsNullOrEmpty(f.Url) && 
                    !string.IsNullOrEmpty(f.Title) &&
                    !string.IsNullOrEmpty(f.Image) && 
                    !string.IsNullOrEmpty(f.Timestamp) &&
                    f.GetTimestamp() > DateTime.MinValue && f.GetTimestamp() < DateTime.MaxValue
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