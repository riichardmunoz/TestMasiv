using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TestCCMasiv.Repositories.Roulette
{
    using TestCCMasiv.Models;
    public interface IRouletteRepository
    {
        Roulette GetById(string rouletteId);
        public Roulette Save(Roulette roulette);
        public Roulette Update(string Id, Roulette roulette);
        public List<Roulette> GetAll();
        bool Close(string rouletteId);
    }
}
