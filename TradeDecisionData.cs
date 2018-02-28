using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitflyerSIM
{
    class TradeDecisionData
    {
        
        public string decision;
        public double price;
        public double lot;

        public void initialize()
        {
            decision = "";
            price = 0;
            lot = 0;
        }
        public TradeDecisionData()
        {
            initialize();
        }


    }
}
