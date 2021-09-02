using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCCMasiv.Models
{
    [Serializable]
    public class ResultBet
    {
        public ResultBet()
        {
            Awards = new List<Prize>();
        }
        public int WinningNumber { get; set; }
        public string WinnigColor { get; set; }
        public List<Prize> Awards { get; set; }
    }
}
