using EFCore.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Repo
{
    public interface IEFCoreRepo
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        Task<IEnumerable<Heroi>> GetAllHerois(bool incluirBatalha = false);
        Task<Heroi> GetHeroiById(int id, bool incluirBatalha = false);
        Task<IEnumerable<Heroi>> GetHeroiByName(string nome, bool incluirBatalha = false);

        Task<IEnumerable<Batalha>> GetAllBatalhas(bool incluirHeroi = false);
        Task<Batalha> GetBatalhaById(int id, bool incluirHeroi = false);
    }
}
