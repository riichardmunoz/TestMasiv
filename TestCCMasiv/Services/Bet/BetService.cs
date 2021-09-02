using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCCMasiv.Models;
using TestCCMasiv.Repositories.Bet;

namespace TestCCMasiv.Services
{
    public class BetService : IBetService
    {
        private IBetRepository betRepository;
        public BetService(IBetRepository betRepository)
        {
            this.betRepository = betRepository;
        }
        public ResultBet CloseBet(string rouletteID)
        {
            return betRepository.CloseBet(rouletteID);
        }
        public Bet CreateBet(Bet bet)
        {
            return betRepository.CreateBet(bet);
        }
    }
}
