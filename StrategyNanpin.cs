using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitflyerSIM
{
    class StrategyNanpin
    {
        //lc > nanpin
        //nanpin: entry when pirce was changed from ave_price more than nanpin, entry lot = num_btc * 2
        public static TradeDecisionData makeDecision(Account ac, int i, double kairi_kijun, double pt, double lc, double nanpin)
        {
            TradeDecisionData tdd = new TradeDecisionData();
            tdd.decision = "None";

            if(lc <= nanpin)
            {
                System.Windows.Forms.MessageBox.Show("Invalid LC & Nanpin in StrategyNanpin, lc has to be larger than nanpin");
            }
            else
            {
                if(ac.getIjiritsu >= 1.0)
                {
                    if(ac.getPosition == "Long")
                    {
                        if(ac.getAvePrice * (1+pt) <= PriceData.close[i]) //check for pt
                        {
                            tdd.decision = "PT Long";
                            tdd.price = 0;
                            tdd.lot = ac.getNumBTC;
                        }
                        else if(ac.getAvePrice * (1-lc) >= PriceData.close[i]) //check for lc
                        {
                            tdd.decision = "LC Long";
                            tdd.price = 0;
                            tdd.lot = ac.getNumBTC;
                        }
                        else if ((ac.getAvePrice * (1 - nanpin) >= PriceData.close[i]) && (ac.getMoney > ac.calcEstimatedRequiredShokokin(PriceData.close[i],ac.getNumBTC * 2.0))) //check for nanpin
                        {
                            tdd.decision = "Nanpin Long";
                            tdd.price = 0;
                            tdd.lot = ac.getNumBTC * 2.0;
                        }
                        else
                        {
                            tdd.decision = "Hold";
                            tdd.price = 0;
                            tdd.lot = 0;
                        }
                    }
                    else if (ac.getPosition == "Short")
                    {
                        if (ac.getAvePrice * (1 - pt) >= PriceData.close[i]) //check for pt
                        {
                            tdd.decision = "PT Short";
                            tdd.price = 0;
                            tdd.lot = ac.getNumBTC;
                        }
                        else if (ac.getAvePrice * (1 + lc) <= PriceData.close[i]) //check for lc
                        {
                            tdd.decision = "LC Short";
                            tdd.price = 0;
                            tdd.lot = ac.getNumBTC;
                        }
                        else if ((ac.getAvePrice * (1 + nanpin) <= PriceData.close[i]) && (ac.getMoney > ac.calcEstimatedRequiredShokokin(PriceData.close[i], ac.getNumBTC * 2.0))) //check for nanpin
                        {
                            tdd.decision = "Nanpin Short";
                            tdd.price = 0;
                            tdd.lot = ac.getNumBTC * 2.0;
                        }
                        else
                        {
                            tdd.decision = "Hold";
                            tdd.price = 0;
                            tdd.lot = 0;
                        }
                    }
                    else if (ac.getPosition == "None")
                    {
                        double kairi = IndexData.getMA1000[i] / PriceData.close[i];
                        if(kairi >= kairi_kijun)
                        {
                            double lot = ac.calcEstimatedMaxLot(i, PriceData.close[i]);
                            tdd.decision = "Entry Long";
                            tdd.price = 0;
                            tdd.lot = lot / 7.1;
                        }
                        else if(kairi <= kairi_kijun * -1)
                        {
                            double lot = ac.calcEstimatedMaxLot(i, PriceData.close[i]);
                            tdd.decision = "Entry Short";
                            tdd.price = 0;
                            tdd.lot = lot / 7.1;
                        }
                        else
                        {
                            tdd.decision = "Hold";
                        }
                    }
                }
            }
            return tdd;
        }
    }
}
