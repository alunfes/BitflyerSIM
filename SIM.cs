using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitflyerSIM
{
    class SIM
    {
        public SIM()
        {

        }

        public SimLog simStrategyNanpin(int from, int to, double pt, double lc, double nanpin, double kairi_kijun, bool writelog)
        {
            SimLog sl = new SimLog();
            if (from > 1000 && to <= PriceData.date.Count-1 && lc > nanpin)
            {
                Account ac = new Account();
                TradeDecisionData tdd = new TradeDecisionData();
                string d = "Hold";
                double lot = 0;
                for (int i = from; i < to; i++)
                {
                    tdd.initialize();
                    if (d == "Entry Long")
                    {
                        string message = Trade.entryLong(ac, i, tdd.lot);
                        if(message == "OK")
                        {

                        }
                        else
                        {

                        }
                        lot = 0;
                        d = "Hold";
                    }
                    else if(d=="Entry Short")
                    {
                        ac.entryShort(lot, i);
                        lot = 0;
                        d = "Hold";
                    }
                    else if(d == "Exit Long")
                    {
                        ac.exitLongPosition(lot, i);
                        d = "Hold";
                    }
                    else if (d == "Exit Short")
                    {
                        ac.exitShortPosition(lot, i);
                        d = "Hold";
                    }

                    tdd = StrategyNanpin.makeDecision(ac, i, kairi_kijun, pt, lc, nanpin);
                    if(tdd.decision == "Entry Long" || tdd.decision == "Nanpin Long")
                    {
                        d = "Entry Long";
                        lot = tdd.lot;
                    }
                    else if (tdd.decision == "Entry Short" || tdd.decision == "Nanpin Short")
                    {
                        d = "Entry Short";
                        lot = tdd.lot;
                    }
                    else if (tdd.decision == "PT Long" || tdd.decision == "LC Long")
                    {
                        d = "Exit Long";
                        lot = 0;
                    }
                    else if (tdd.decision == "PT Short" || tdd.decision == "LC Short")
                    {
                        d = "Exit Short";
                        lot = 0;
                    }
                    else if(tdd.decision == "Hold")
                    {
                        d = "Hold";
                        lot = 0;
                    }
                    ac.moveToNext(i);
                }
            }
            else
                System.Windows.Forms.MessageBox.Show("Invalid input values in simStrategyNanin!");
            
            return sl;
        }
    }
}
