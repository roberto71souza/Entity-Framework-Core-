using EFCore.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Repo
{
    public class EFCoreRepo : IEFCoreRepo
    {
        private readonly HeroiContext _context;

        public EFCoreRepo(HeroiContext context)
        {
            _context = context;
        }


        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Heroi>> GetAllHerois(bool incluirBatalha)
        {
            IQueryable<Heroi> query = _context.Heroi.Include(x => x.Identidade).Include(x => x.Armas);

            if (incluirBatalha)
            {
                query = query.Include(x => x.HeroiBatalhas).ThenInclude(x => x.Batalha);
            }

            query = query.OrderBy(x => x.Id).AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<Heroi> GetHeroiById(int id, bool incluirBatalha)
        {
            IQueryable<Heroi> heroi = _context.Heroi.Include(x => x.Armas).Include(x => x.Identidade);

            if (incluirBatalha)
            {
                heroi = heroi.Include(x => x.HeroiBatalhas).ThenInclude(x => x.Batalha);
            }
            return await heroi.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Heroi>> GetHeroiByName(string nome, bool incluirBatalha)
        {
            IQueryable<Heroi> heroi = _context.Heroi.Include(x => x.Armas).Include(x => x.Identidade);

            if (incluirBatalha)
            {
                heroi = heroi.Include(x => x.HeroiBatalhas).ThenInclude(x => x.Batalha);
            }

            heroi = heroi.Where(x => x.Nome.Contains(nome)).OrderBy(x => x.Id).AsNoTracking();
            return await heroi.ToListAsync();
        }

        /*Batalha*/

        public async Task<IEnumerable<Batalha>> GetAllBatalhas(bool incluirHeroi = false)
        {
            IQueryable<Batalha> batalha = _context.Batalha;
            if (incluirHeroi)
            {
                batalha = batalha.Include(z => z.HeroiBatalhas).ThenInclude(x => x.Heroi);
            }
            return await batalha.OrderBy(x => x.Id).AsNoTracking().ToListAsync();
        }

        public async Task<Batalha> GetBatalhaById(int id, bool incluirHeroi = false)
        {
            IQueryable<Batalha> batalha = _context.Batalha;

            if (incluirHeroi)
            {
                batalha = batalha.Include(x => x.HeroiBatalhas).ThenInclude(x => x.Heroi);
            }
            return await batalha.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
