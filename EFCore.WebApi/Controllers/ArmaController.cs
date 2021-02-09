using EFCore.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArmaController : ControllerBase
    {
        public HeroiContext _context { get; }

        public ArmaController(HeroiContext heroiContext)
        {
            _context = heroiContext;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok();
        }

        [HttpPost]
        public ActionResult Post()
        {
            return Ok();
        }

        [HttpPut]
        public ActionResult Put()
        {
            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete()
        {
            return Ok();
        }

    }
}
