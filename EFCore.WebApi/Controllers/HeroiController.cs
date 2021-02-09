using EFCore.Domain;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HeroiController : ControllerBase
    {
        public IEFCoreRepo _context { get; }

        public HeroiController(IEFCoreRepo heroiContext)
        {
            _context = heroiContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listaHeroi = await _context.GetAllHerois(true);

                if (listaHeroi.Count() > 0)
                {
                    return Ok(listaHeroi);
                }
                return NotFound(listaHeroi.Count());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetByName/{nome}")]
        public async Task<IActionResult> GetByName(string nome)
        {
            try
            {
                var heroi = await _context.GetHeroiByName(nome, true);

                if (heroi.Count() > 0)
                {
                    return Ok(heroi);
                }
                return NotFound("Heroi nao encontrado!!");

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Heroi model)
        {
            try
            {
                if (model != null)
                {
                    _context.Add(model);
                    if (await _context.SaveChangesAsync())
                    {
                        return Ok();
                    }
                    return BadRequest($"Erro ao adicionar heroi {model.Nome}");
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Heroi model)
        {
            try
            {
                var heroi = await _context.GetHeroiById(model.Id);
                if (heroi != null)
                {
                    _context.Update(model);
                    if (await _context.SaveChangesAsync())
                    {
                        return Ok();
                    }
                    return BadRequest();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var heroi = await _context.GetHeroiById(id);

                if (heroi != null)
                {
                    _context.Delete(heroi);

                    if (await _context.SaveChangesAsync())
                    {
                        return Ok();
                    }
                    return BadRequest();
                }
                return NotFound();

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
