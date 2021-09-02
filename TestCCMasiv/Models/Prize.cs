using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCCMasiv.Models
{
    [Serializable]
    public class Prize
    {
        public int Number { get; set; }
        public string Color { get; set; }
        public double Amount { get; set; }
        public double Gain { get; set; }
    }
}
