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
        public static string entryLong(Account ac, int i, double lot)
        {
            //check required shokokin
            if (checkExecution(ac, i, lot))
            {
                ac.entryLong(lot, i);
                return "OK";
            }
            else
                return "Failed Long Entry: Required Shokokin is not sufficient";
        }

        public static string entryShort(Account ac, int i, double lot)
        {
            //check required shokokin
            if (checkExecution(ac, i, lot))
            {
                ac.entryShort(lot, i);
                return "OK";

            }
            else
                return "Failed Short Sashine Entry: Required Shokokin is not sufficient";
        }
        #endregion


        #region Exit
        public static string exitLong(Account ac, int i, double lot)
        {
            if (ac.getNumBTC >= lot)
            {
                string res = ac.exitLongPosition(lot, i);
                if (res != "OK")
                    return res;
                else
                    return res;
            }
            else
                return "Failed Long Exit: Can't sell more than hold";

        }

        public static string exitShort(Account ac, int i, double lot)
        {
            if (ac.getNumBTC >= lot)
            {
                string res = ac.exitShortPosition(lot, i);
                if (res != "OK")
                    return res;
                else
                    return res;
            }
            else
                return "Failed Short Exit: Can't buy more than hold";

        }
        #endregion



        /*private static bool checkIjiritsu(Account ac, int i, double entry_price, double lot)
        {
            double estimated_required_shokokin = ((ac.getAvePrice * ac.getNumBTC) + (entry_price * lot)) / SystemData.leverage;
            double estimated_ijiritsu = ((ac.getAvePrice * ac.getNumBTC) + (entry_price * lot)) / (estimated_required_shokokin + ac.getPL);
            if (estimated_ijiritsu <= 0.8)
                return false;
            else
                return true;
        }*/

        //check possibility of execution
        //true=executable, false=not executable
        private static bool checkExecution(Account ac, int i, double lot)
        {
            double reqshokokin = ac.calcEstimatedRequiredShokokin(PriceData.open[i], lot);
            if (ac.getMoney > reqshokokin)
                return true;
            else
                return false;
        }
    }
}
