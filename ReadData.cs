using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace BitflyerSIM
{
    class ReadData
    {
        public static void readTickData()
        {
            TickData.initialize();

            using (System.IO.StreamReader sr = new System.IO.StreamReader("tick.csv", Encoding.UTF8, false))
            {
                try
                {
                    int num = 0;
                    foreach (var line in File.ReadLines("tick.csv"))
                    {
                        var e = line.Split(',');
                        TickData.time.Add(FromUnixTime(Convert.ToInt64(e[0])));
                        TickData.price.Add(Convert.ToDouble(e[1]));
                        TickData.volume.Add(Convert.ToDouble(e[2]));
                        num++;
                    }
                    Form1.Form1Instance.setLabel("Read Completed Num= " + num.ToString());
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.ToString());
                }
            }
        }

        public static void readPriceData(object file_name)
        {
            PriceData.initialize();
            
            try
            {
                int num = 0;
                foreach (var line in File.ReadLines(file_name.ToString()))
                {
                    var e = line.Split(',');
                    PriceData.date.Add(Convert.ToDateTime(e[0]));
                    PriceData.open.Add(Convert.ToDouble(e[1]));
                    PriceData.high.Add(Convert.ToDouble(e[2]));
                    PriceData.low.Add(Convert.ToDouble(e[3]));
                    PriceData.close.Add(Convert.ToDouble(e[4]));
                    PriceData.volume.Add(Convert.ToDouble(e[5]));
                    num++;
                }
                Form1.Form1Instance.setLabel("Read "+file_name.ToString() + " Completed Num= " + num.ToString());
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }

        }

        private static DateTime FromUnixTime(long unixTime)
        {
            return DateTimeOffset.FromUnixTimeSeconds(unixTime).LocalDateTime;
        }
    }
}
