using EFCore.Domain;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.WebApi.Controllers
{
    [ApiController]
    [Route("{controler}")]
    public class BatalhaController : ControllerBase
    {
        public IEFCoreRepo _context { get; set; }
        public BatalhaController(IEFCoreRepo coreRepo)
        {
            _context = coreRepo;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var listaBatalha = await _context.GetAllBatalhas(true);
                if (listaBatalha.Count() > 0)
                {
                    return Ok(listaBatalha);
                }
                else
                {
                    return NotFound(listaBatalha.Count());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar(Batalha model)
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
                    return BadRequest("Erro");
                }
                return Ok("Erro objeto nulo");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Atualizar(Batalha model)
        {
            try
            {
                var batalha = await _context.GetBatalhaById(model.Id);

                if (batalha != null)
                {
                    _context.Update(model);

                    if (await _context.SaveChangesAsync())
                    {
                        return Ok($"Batalha {model.Nome} foi atualizada com sucesso!!");
                    }
                    return BadRequest("Erro ao atualizar");
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Deletar(int id)
        {
            try
            {
                var batalha = await _context.GetBatalhaById(id);

                if (batalha != null)
                {
                    _context.Delete(batalha);
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