using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCCMasiv.Repositories.Roulette
{
    using TestCCMasiv.Repositories;
    using TestCCMasiv.Models;
    using EasyCaching.Core;

    public class RouletteRepository : IRouletteRepository
    {
        private IEasyCachingProviderFactory cachingProviderFactory;
        private IEasyCachingProvider cachingProvider;
        private const string KEYREDIS = "TROULETE";
        string openStatus, closeStatus;

        public RouletteRepository(IEasyCachingProviderFactory cachingProviderFactory)
        {
            this.cachingProviderFactory = cachingProviderFactory;
            this.cachingProvider = this.cachingProviderFactory.GetCachingProvider("roulette");
            this.openStatus = Environment.GetEnvironmentVariable("OpenStatus").ToString();
            this.closeStatus = Environment.GetEnvironmentVariable("CloseStatus").ToString();
        }
        public Roulette Save(Roulette roulette)
        {
            cachingProvider.Set(KEYREDIS + roulette.Id, roulette, TimeSpan.FromDays(365));

            return roulette;
        }

        public Roulette GetById(string rouletteId)
        {
            var item = this.cachingProvider.Get<Roulette>(KEYREDIS + rouletteId);
            if (!item.HasValue)
            {
                return null;
            }

            return item.Value;
        }

        public Roulette Update(string Id, Roulette roulette)
        {
            roulette.Id = Id;

            return Save(roulette);
        }

        public List<Roulette> GetAll()
        {
            var rouletes = this.cachingProvider.GetByPrefix<Roulette>(KEYREDIS);
            if (rouletes.Values.Count == 0)
            {
                return new List<Roulette>();
            }

            return new List<Roulette>(rouletes.Select(x => x.Value.Value));
        }

        public bool Close(string rouletteId)
        {
            var roulette = GetById(rouletteId: rouletteId);
            if (roulette==null)
                throw new Exception(Utils.Utils.CreateMessageError("La ruleta no existe."));
            if (roulette.Status != this.openStatus)
                throw new Exception(Utils.Utils.CreateMessageError("La ruleta no se encuentra abierta."));
            roulette.Status = this.closeStatus;
            Roulette eRoulette= Save(roulette);
            if (eRoulette!=null)
            {
                return true;
            }

            return false;
        }
    }
}
