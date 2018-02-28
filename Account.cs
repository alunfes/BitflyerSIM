using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitflyerSIM
{
    /*トレードは1つの足に対して一回のみ。*/
    class Account
    {
        private double asset;
        private double money;
        private double ave_price;
        private double num_btc;
        private string position;//long, short, none, loss cut
        private double pl;
        private int num_trade;
        private double cum_pl;//should be added only when close position
        private double required_shokokin;
        private double ijiritsu;

        private Dictionary<int, double> asset_log;
        private Dictionary<int, double> money_log;
        private Dictionary<int, double> ave_price_log;
        private Dictionary<int, double> num_btc_log;
        private Dictionary<int, string> position_log;
        private Dictionary<int, double> pl_log;
        private Dictionary<int, int> num_trade_log;
        private Dictionary<int, double> cum_pl_log;
        private Dictionary<int, double> required_shokokin_log;
        private Dictionary<int, double> ijiritsu_log;
        private Dictionary<int, string> ac_message_log;

        public double getAsset { get { return asset; } }
        public double getMoney { get { return money; } }
        public double getAvePrice { get { return ave_price; } }
        public double getNumBTC { get { return num_btc; } }
        public string getPosition { get { return position; } }
        public double getPL { get { return pl; } }
        public int getNumTrade { get { return num_trade; } }
        public double getCumPL { get { return cum_pl; } }
        public double getRequiredShokokin { get { return required_shokokin; } }
        public double getIjiritsu { get { return ijiritsu; } }



        public Account()
        {
            initialize();
        }

        public void initialize()
        {
            asset = 1000000;
            money = 1000000;
            ave_price = 0;
            num_btc = 0;
            pl = 0;
            num_trade = 0;
            cum_pl = 0;
            required_shokokin = 0;
            ijiritsu = 0;

            asset_log = new Dictionary<int, double>();
            money_log = new Dictionary<int, double>();
            ave_price_log = new Dictionary<int, double>();
            num_btc_log = new Dictionary<int, double>();
            position_log = new Dictionary<int, string>();
            pl_log = new Dictionary<int, double>();
            num_trade_log = new Dictionary<int, int>();
            cum_pl_log = new Dictionary<int, double>();
            required_shokokin_log = new Dictionary<int, double>();
            ijiritsu_log = new Dictionary<int, double>();
            ac_message_log = new Dictionary<int, string>();
        }

        #region Entry
        public bool entryLong(double num, double price, int i)
        {
            if (money > num * price && position != "Short")
            {
                money -= num * price;
                ave_price = ((ave_price * num_btc) + (num * price)) / (num_btc + num);
                num_btc += num;
                position = "Long";
                num_trade++;
                ac_message_log.Add(i, "OK:Long Entry");
                //required_shokokin = calcRequiredShokokin(i);
                //ijiritsu = calcIjiritsu(i);
                return true;
            }
            else
            {
                ac_message_log.Add(i, "Failed:Long Entry");
                return false;
            }
        }

        public bool entryShort(double num, double price, int i)
        {
            if (money > num * price && position != "Long")
            {
                money -= num * price;
                ave_price = ((ave_price * num_btc) + (num * price)) / (num_btc + num);
                num_btc += num;
                position = "Short";
                num_trade++;
                ac_message_log.Add(i, "OK:Short Entry");
                //required_shokokin = calcRequiredShokokin(i);
                //ijiritsu = calcIjiritsu(i);
                return true;
            }
            else
            {
                ac_message_log.Add(i, "Failed:Short Entry");
                return false;
            }
        }
        #endregion

        #region ClosePosition
        public string closeLongPosition(double num, double close_price, int i)
        {
            if (num_btc >= num)
            {
                cum_pl += (ave_price - close_price) * num;
                money += (ave_price - close_price) * num;
                pl = 0;
                num_btc -= num;
                num_trade++;
                asset = calcAsset(i);
                if (num_btc > 0)
                    position = "Long";
                else
                    position = "None";

                ac_message_log.Add(i, "OK:Long Exit");
                return "OK";
            }
            else //over num error
            {
                ac_message_log.Add(i, "Failed:Long Exit");
                return "Failed Close Long: Can't sell num more than hold";
            }
        }

        public string closeShortPosition(double num, double close_price, int i)
        {
            if (num_btc >= num)
            {
                cum_pl += (close_price - ave_price) * num;
                money += (close_price - ave_price) * num;
                pl = 0;
                num_btc -= num;
                num_trade++;
                asset = calcAsset(i);
                if (num_btc > 0)
                    position = "Short";
                else
                    position = "None";

                ac_message_log.Add(i, "OK:Short Exit");
                return "OK";
            }
            else //over num error
            {
                ac_message_log.Add(i, "Failed:Short Exit");
                return "Failed Close Short: Can't buy num more than hold";
            }
        }
        #endregion


        //make log on every candle stick
        private void logAC(int i)
        {
            asset_log.Add(i, asset);
            money_log.Add(i, money);
            ave_price_log.Add(i, ave_price);
            num_btc_log.Add(i, num_btc);
            position_log.Add(i, position);
            pl_log.Add(i, pl);
            num_trade_log.Add(i, num_trade);
            cum_pl_log.Add(i, cum_pl);
            required_shokokin_log.Add(i, required_shokokin);
            ijiritsu_log.Add(i, ijiritsu);
        }

        public void moveToNext(int i)
        {
            calcPL(i);
            asset = calcAsset(i);
            ave_price_log.Add(i, ave_price);
            required_shokokin = calcRequiredShokokin(i);
            ijiritsu = calcIjiritsu(i);
            checkLossCut(i);
            logAC(i);
        }

        public void forceExit(int i)
        {
            if (position == "Long")
            {
                closeLongPosition(getNumBTC, PriceData.close[i], i);
            }
            else if(position == "Short")
            {
                closeShortPosition(getNumBTC, PriceData.close[i], i);
            }
        }

        private double calcAsset(int i)
        {
            return money + pl + num_btc * PriceData.close[i];
        }



        private double calcRequiredShokokin(int i)
        {
            return (ave_price * num_btc) / SystemData.leverage;
        }

        private double calcIjiritsu(int i)
        {
            return (ave_price * num_btc) / (required_shokokin + pl);
        }

        private void calcPL(int i)
        {
            if (position == "long")
            {
                pl = (PriceData.close[i] - ave_price) * getNumBTC;
            }
            else if (position == "short")
            {
                pl = (ave_price - PriceData.close[i]) * getNumBTC;
            }
            else
            {
                pl = 0;
            }
        }

        //true = loss cut, false=no loss cut
        private bool checkLossCut(int i)
        {
            if (required_shokokin >= asset)
            {
                return true;
            }
            else if (ijiritsu <= 0.8)
            {
                doLossCut(i);
                return false;
            }
            else
                return false;
        }

        private void doLossCut(int i)
        {
            if (position == "Long")
            {
                ac_message_log.Add(i, "Loss cut long");
                closeLongPosition(getNumBTC, PriceData.close[i], i);
            }
            else if (position == "Short")
            {
                ac_message_log.Add(i, "Loss cut short");
                closeShortPosition(getNumBTC, PriceData.close[i], i);
            }
            else
                System.Windows.Forms.MessageBox.Show("Unknown position code in doLossCut");

            position = "Loss Cut";
        }
    }
}
