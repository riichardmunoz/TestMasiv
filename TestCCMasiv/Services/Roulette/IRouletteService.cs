using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCCMasiv.Services
{
    using TestCCMasiv.Models;
    public interface IRouletteService
    {
        public Roulette create();
        public Roulette OpenRoulette(string Id);
        public List<Roulette> GetAll();
    }
}
