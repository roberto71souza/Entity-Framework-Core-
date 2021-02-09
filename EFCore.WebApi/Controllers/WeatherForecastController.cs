using EFCore.Domain;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IEFCoreRepo _context;

        public WeatherForecastController(IEFCoreRepo coreRepo)
        {
            _context = coreRepo;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("{nameH}")]
        public async Task<ActionResult> Get(string nameH)
        {
            var heroi = new Heroi() { Nome = nameH };

            _context.Add(heroi);

            if (await _context.SaveChangesAsync())
            {
                return Ok($"heroi {heroi.Nome} foi salvo");
            }
            return Ok($"Error");
        }
        /*    {
            "nome" : "Batalha Nova York",
            "descricao" : "Primeira batalha dos vingadores",
            "dtInicio" : "2015-12-26T00:00:00",
            "dtFim" : "2016-12-16T00:00:00",
        "heroiBatalhas":[
        {
        "heroiId": 1
        },
        {
        "heroiId": 2
        }
        ]
    }*/

        /* [HttpGet("GetHerois")]
         public ActionResult GetHerois()
         {
             // var resultGet = _context.Heroi.ToList();
             IEnumerable<Heroi> resultGet = from heroi in _context.Heroi select heroi;

             return Ok(resultGet);
         }

         [HttpGet("Filtro/{filtro}")]
         public ActionResult GetHerois(string filtro)
         {
             var resultFiltro = _context.Heroi.Where(x => x.Nome == filtro).ToList();
             //IEnumerable<Heroi> resultFiltro = from heroi in _context.Heroi where heroi.Nome.Contains(filtro) select heroi;
             if (resultFiltro.Count() > 0)
             {
                 return Ok(resultFiltro);
             }
             return NotFound();
         }

         [HttpGet("update/{id}&{newName}")]
         public ActionResult UpdateHeroi(int id,string newName)
         {
             var resultS = _context.Heroi.Where(x => x.Id == id).FirstOrDefault();

             if (resultS != null)
             {
                 resultS.Nome = newName;
                 _context.Heroi.Update(resultS);
                 _context.SaveChanges();
                 return Ok("Nome Alterado");
             }
             return NotFound("Nao encontrado");
         }

         [HttpGet("Delete/{id}")]
         public ActionResult Delete(int id)
         {
             var resultHeroi = _context.Heroi.Where(x => x.Id == id).FirstOrDefault();

             if (resultHeroi != null)
             {
                 _context.Heroi.Remove(resultHeroi);
                 _context.SaveChanges();
                 return Ok($"{resultHeroi.Nome} foi removido.");
             }
             return NotFound("Nao encontrado!");
         }*/
    }
}
