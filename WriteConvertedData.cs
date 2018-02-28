using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace BitflyerSIM
{
    class WriteConvertedData
    {
        public static void writeData(object file_name)
        {
            using (StreamWriter sw = new StreamWriter(file_name.ToString(),false,Encoding.Default))
            {
                sw.WriteLine("DateTime" + ',' + "open" + ',' + "high" + ',' + "low" + ',' + "close"+','+ "volume");
                for (int i = 0; i < PriceData.date.Count - 1; i++)
                {
                    sw.WriteLine(PriceData.date[i].ToString() +',' + PriceData.open[i].ToString() + ',' + PriceData.high[i].ToString() + ',' + 
                        PriceData.low[i].ToString() + ','+PriceData.close[i].ToString() + ',' + PriceData.volume[i].ToString());
                }
                Form1.Form1Instance.setLabel("Completed write data");
            }
        }
    }
}
