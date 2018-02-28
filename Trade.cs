using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitflyerSIM
{
    class Trade
    {
        #region Entry
        public static string longSashineEntry(Account ac, int i, double entry_price, double lot)
        {
            //check required shokokin
            if (checkIjiritsu(ac, i, entry_price, lot))
            {
                if (checkExecution(i, entry_price, lot))
                {
                    ac.entryLong(lot, entry_price, i);
                    return "OK";
                }
                else
                    return "Failed Long Sashine Entry: Non Executable price or volume";
            }
            else
                return "Failed Long Sashine Entry: Required Shokokin is not sufficient";
        }

        public static string shortSashineEntry(Account ac, int i, double entry_price, double lot)
        {
            //check required shokokin
            if (checkIjiritsu(ac, i, entry_price, lot))
            {
                if (checkExecution(i, entry_price, lot))
                {
                    ac.entryShort(lot, entry_price, i);
                    return "OK";
                }
                else
                    return "Failed Short Sashine Entry: Non Executable price or volume";
            }
            else
                return "Failed Short Sashine Entry: Required Shokokin is not sufficient";
        }
        #endregion


        #region Exit
        public static string exitLong(Account ac, int i, double exit_price, double lot)
        {
            if (ac.getNumBTC >= lot)
            {
                if (checkExecution(i, exit_price, lot))
                {
                    string res = ac.closeLongPosition(lot, exit_price, i);
                    if (res != "OK")
                        return res;
                    else
                        return res;

                }
                else
                    return "Failed Exit Long: Can't sell more than hold";
            }
            else
                return "Failed Long Exit: Can't sell more than hold";

        }

        public static string exitShort(Account ac, int i, double exit_price, double lot)
        {
            if (ac.getNumBTC >= lot)
            {
                if (checkExecution(i, exit_price, lot))
                {
                    string res = ac.closeShortPosition(lot, exit_price, i);
                    if (res != "OK")
                        return res;
                    else
                        return res;

                }
                else
                    return "Failed Exit Short: Can't buy more than hold";
            }
            else
                return "Failed Short Exit: Can't buy more than hold";

        }
        #endregion



        private static bool checkIjiritsu(Account ac, int i, double entry_price, double lot)
        {
            double estimated_required_shokokin = ((ac.getAvePrice * ac.getNumBTC) + (entry_price * lot)) / SystemData.leverage;
            double estimated_ijiritsu = ((ac.getAvePrice * ac.getNumBTC) + (entry_price * lot)) / (estimated_required_shokokin + ac.getPL);
            if (estimated_ijiritsu <= 0.8)
                return false;
            else
                return true;
        }

        //check possibility of execution
        //true=executable, false=not executable
        private static bool checkExecution(int i, double entry_price, double lot)
        {
            if (PriceData.close[i] >= entry_price && PriceData.low[i] <= entry_price && PriceData.volume[i] >= entry_price * lot * 2)
                return true;
            else
                return false;
        }
    }
}
