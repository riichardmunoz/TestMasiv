using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TestCCMasiv.Repositories.Bet
{
    using TestCCMasiv.Models;
    public interface IBetRepository
    {
        public Bet CreateBet(Bet bet);
        public ResultBet CloseBet(string rouletteID);
    }
}
