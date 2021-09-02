using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCCMasiv.Models;

namespace TestCCMasiv.Services
{
    public interface IBetService
    {
        public Bet CreateBet(Bet bet);
        ResultBet CloseBet(string rouletteID);
    }
}
