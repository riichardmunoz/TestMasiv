using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCCMasiv.Models;
using TestCCMasiv.Repositories.Roulette;

namespace TestCCMasiv.Services
{
    public class RouletteService : IRouletteService
    {
        private IRouletteRepository rouletteRepository;
        string openStatus, closeStatus;
        public RouletteService(IRouletteRepository rouletteRepository)
        {
            this.rouletteRepository = rouletteRepository;
            this.openStatus = Environment.GetEnvironmentVariable("OpenStatus").ToString();
            this.closeStatus = Environment.GetEnvironmentVariable("CloseStatus").ToString();
        }
        public Roulette create()
        {
            Roulette roulette = new Roulette()
            {
                Id = Guid.NewGuid().ToString(),
                CreationDate= DateTime.Now
            };
            rouletteRepository.Save(roulette);

            return roulette;
        }

        public Roulette OpenRoulette(string Id)
        {
            Roulette roulette = rouletteRepository.GetById(Id);
            if (roulette == null)
            {
                throw new Exception(Utils.Utils.CreateMessageError(message: "La ruleta que desea abrir no existe."));
            }

            if (roulette.Status == this.closeStatus)
            {
                throw new Exception(Utils.Utils.CreateMessageError(message: "La ruleta se encuentra cerrada."));
            }
            roulette.Status = this.openStatus;
            roulette.CreationDate = DateTime.Now;

            return rouletteRepository.Update(Id, roulette);
        }

        public List<Roulette> GetAll()
        {
            return rouletteRepository.GetAll();
        }
    }
}
