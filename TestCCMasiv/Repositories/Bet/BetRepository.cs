using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCCMasiv.Repositories.Bet
{
    using TestCCMasiv.Repositories;
    using TestCCMasiv.Models;
    using TestCCMasiv.Repositories.Roulette;
    using EasyCaching.Core;
    using StackExchange.Redis;

    public class BetRepository : IBetRepository
    {
        IRouletteRepository rouletteRepository;
        private IEasyCachingProviderFactory cachingProviderFactory;
        private IEasyCachingProvider cachingProvider;
        private const string KEYREDIS = "TBET";
        ResultBet resultBets = null;
        string openStatus, closeStatus;

        public BetRepository( IEasyCachingProviderFactory cachingProviderFactory)
        {
            this.cachingProviderFactory = cachingProviderFactory;
            this.rouletteRepository = new RouletteRepository(cachingProviderFactory);
            this.cachingProvider = this.cachingProviderFactory.GetCachingProvider("roulette");
            this.openStatus = Environment.GetEnvironmentVariable("OpenStatus").ToString();
            this.closeStatus = Environment.GetEnvironmentVariable("CloseStatus").ToString();
        }

        public ResultBet CloseBet(string rouletteID)
        {
            var betsRoulette = GetAllByRouletteId(rouletteID);
            bool cerrada =rouletteRepository.Close(rouletteID);
            if (betsRoulette.Count>0)
            {
                SelectWinner(betsRoulette);
            }

            return resultBets;
        }
        private void SelectWinner(List<Bet> betsList)
        {
            List<Bet> betsByNumber = betsList.Where(x => x.Number != 0).ToList();
            resultBets = new ResultBet();
            int index = new Random().Next(betsByNumber.Count);
            Bet winningBet = betsByNumber[index];
            string winningColor = (winningBet.Number % 2 == 0 ? "Rojo" : "Negro");
            resultBets.WinnigColor = winningColor;
            resultBets.WinningNumber = winningBet.Number;
            resultBets.Awards.AddRange(CalculateProfitNumber(betsList));
            resultBets.Awards.AddRange(CalculateProfitColor(betsList));
        }
        private List<Prize> CalculateProfitNumber(List<Bet> betsList)
        {
            double GainFactorPerNumber = Convert.ToDouble(Environment.GetEnvironmentVariable("GainXNumber"));

            return betsList.Where(b => b.Number == resultBets.WinningNumber)
                   .Select(c => new Prize()
                   {
                       Number = c.Number,
                       Amount = c.Amount,
                       Gain = c.Amount * GainFactorPerNumber
                   }).ToList();
        }
        private List<Prize> CalculateProfitColor(List<Bet> betsList)
        {
            double GainFactorPerColor = Convert.ToDouble(Environment.GetEnvironmentVariable("GainXColor"));

            return betsList.Where(b => b.Color == resultBets.WinnigColor)
                   .Select(c => new Prize()
                   {
                       Color = c.Color,
                       Amount = c.Amount,
                       Gain = c.Amount * GainFactorPerColor
                   }).ToList();
        }
        private List<Bet> GetAllByRouletteId(string rouletteId)
        {
            List<Bet> lBets = this.GetAll();
            if (lBets.Count <= 0)
                return new List<Bet>();

            return (lBets.Where(b => b.RouletteId == rouletteId).ToList());
        }

        private List<Bet> GetAll()
        {
            var lBet = this.cachingProvider.GetByPrefix<Bet>(KEYREDIS);

            return new List<Bet>(lBet.Select(x => x.Value.Value));
        }

        public Bet CreateBet(Bet bet)
        {
            
            if (!IsValidRoulette(bet.RouletteId))
                throw new Exception(Utils.Utils.CreateMessageError(message: "La apuesta se intenta realizar en una ruleta que no existe o no esta abierta."));
            if (!ValidateColor(bet.Color))
                throw new Exception(Utils.Utils.CreateMessageError(message: "Los colores válidos son [Rojo] y [Negro]"));
            Bet betEntity = new Bet()
            {
                Id = bet.Id,
                Number =bet.Number,
                Color=bet.Color,
                Amount=bet.Amount,
                RouletteId=bet.RouletteId,
                IdUsuario=bet.IdUsuario
            };

            return Save(betEntity);
        }

        private Bet Save(Bet betEntity)
        {
            cachingProvider.Set(KEYREDIS + betEntity.Id, betEntity, TimeSpan.FromDays(365));

            return betEntity;
        }

        private bool IsValidRoulette(string idRoulette)
        {
            Roulette roulette = rouletteRepository.GetById(idRoulette);
            if (roulette!=null)
            {
                return (roulette.Status == this.openStatus);
            }
            return false;

        }
        private bool ValidateColor(string color)
        {
            if (string.IsNullOrEmpty(color))
                return false;
            switch (color.ToLower())
            {
                case "negro":
                case "rojo":

                    return true;
                default:

                    return false;
            }
        }
    }
}
