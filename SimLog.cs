using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitflyerSIM
{
    class SimLog
    {
        public List<int> i_log;
        public Dictionary<int, string> decision_log;
        public Dictionary<int, string> trade_log;
        public Dictionary<int, double> money_log;
        public Dictionary<int, double> trade_price_log;
        public Dictionary<int, double> ave_price_log;
        public Dictionary<int, double> num_btc_log;
        public Dictionary<int, double> pl_log;
        public Dictionary<int, double> cum_pl_log;
        public Dictionary<int, string> position_log;
        public Dictionary<int, int> num_trade_log;
        public Dictionary<int, double> required_shokokin_log;
        public Dictionary<int, double> ijiritsu_log;

        public SimLog()
        {
            initialize();
        }

        public void initialize()
        {
            i_log = new List<int>();
            decision_log = new Dictionary<int, string>();
            trade_log = new Dictionary<int, string>();
            money_log = new Dictionary<int, double>();
            trade_price_log = new Dictionary<int, double>();
            ave_price_log = new Dictionary<int, double>();
            num_btc_log = new Dictionary<int, double>();
            pl_log = new Dictionary<int, double>();
            cum_pl_log = new Dictionary<int, double>();
            position_log = new Dictionary<int, string>();
            num_trade_log = new Dictionary<int, int>();
            required_shokokin_log = new Dictionary<int, double>();
            ijiritsu_log = new Dictionary<int, double>();
        }

        public void takeDailyLog(Account ac, int i)
        {
            i_log.Add(i);
            money_log.Add(i, ac.getMoney);
            ave_price_log.Add(i, ac.getAvePrice);
            num_btc_log.Add(i, ac.getNumBTC);
            pl_log.Add(i, ac.getPL);
            cum_pl_log.Add(i, ac.getCumPL);
            position_log.Add(i, ac.getPosition);
            num_trade_log.Add(i, ac.getNumTrade);
            required_shokokin_log.Add(i, ac.getRequiredShokokin);
            ijiritsu_log.Add(i, ac.getIjiritsu);
        }

    }
}
