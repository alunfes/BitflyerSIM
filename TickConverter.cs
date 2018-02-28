using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitflyerSIM
{
    class TickConverter
    {
        public static void converterTo1M()
        {
            PriceData.initialize();
            double high = 0;
            double low = 9999999;
            double open = 0;
            double volume = 0;
            double close = 0;

            //detect start datetime and index
            DateTime start_dt = TickData.time[0];
            start_dt = start_dt.AddMinutes(1);
            start_dt = start_dt.AddSeconds(start_dt.Second * -1);

            int start = 0;
            while (TickData.time[start] <= start_dt)
                start++;


            DateTime kijun_dt = new DateTime();
            kijun_dt = start_dt.AddMinutes(1);
            int num = 0;
            int numd = 0;
            for (int i = start; i < TickData.time.Count - 1; i++)
            {
                if (TickData.time[i] < kijun_dt)
                {
                    if (num == 0)
                    {
                        open = TickData.price[i];
                        high = 0;
                        low = 999999999;
                    }

                    high = Math.Max(high, TickData.price[i]);
                    low = Math.Min(low, TickData.price[i]);
                    volume = volume + (TickData.volume[i] * TickData.price[i]);
                    num++;
                }
                else
                {
                    PriceData.open.Add(open);
                    PriceData.high.Add(high);
                    PriceData.low.Add(low);
                    PriceData.close.Add(TickData.price[i-1]);
                    PriceData.volume.Add(volume);
                    PriceData.date.Add(TickData.time[i]);
                    num = 0;
                    open = 0;
                    high = 0;
                    low = 9999999;
                    close = 0;
                    volume = 0;
                    i--;
                    numd++;
                    kijun_dt = kijun_dt.AddMinutes(1);
                }
            }
            Form1.Form1Instance.setLabel("Completed convert. Num= " + numd.ToString());
        }
    }
}
