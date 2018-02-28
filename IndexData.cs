using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitflyerSIM
{
    class IndexData
    {
        private static List<double> ma25;
        private static List<double> ma50;
        private static List<double> ma100;
        private static List<double> ma200;
        private static List<double> ma500;
        private static List<double> ma1000;
        private static List<double> ma3000;
        private static List<double> ma6000;
        private static List<double> ma12000;
        private static List<double> ma24000;

        public static List<double> getMA25 { get { return ma25; } }
        public static List<double> getMA50 { get { return ma50; } }
        public static List<double> getMA100 { get { return ma100; } }
        public static List<double> getMA200 { get { return ma200; } }
        public static List<double> getMA500 { get { return ma500; } }
        public static List<double> getMA1000 { get { return ma1000; } }
        public static List<double> getMA3000 { get { return ma3000; } }
        public static List<double> getMA6000 { get { return ma6000; } }
        public static List<double> getMA12000 { get { return ma12000; } }
        public static List<double> getMA24000 { get { return ma24000; } }

        public static void initialize()
        {
            ma25 = new List<double>();
            ma50 = new List<double>();
            ma100 = new List<double>();
            ma200 = new List<double>();
            ma500 = new List<double>();
            ma1000 = new List<double>();
            ma3000 = new List<double>();
            ma6000 = new List<double>();
            ma12000 = new List<double>();
            ma24000 = new List<double>();
            calcAllMA();
        }

        private static void calcAllMA()
        {
            ma25 = calcMA(25);
            ma50 = calcMA(50);
            ma100 = calcMA(100);
            ma200 = calcMA(200);
            ma500 = calcMA(500);
            ma1000 = calcMA(1000);
            ma3000 = calcMA(3000);
            ma6000 = calcMA(6000);
            ma12000 = calcMA(12000);
            ma24000 = calcMA(24000);
        }

        private static List<double> calcMA(int ma_num)
        {
            List<double> res = new List<double>();

            for (int i = ma_num; i < PriceData.date.Count - 1; i++)
            {
                double sum = 0;
                for (int j = i - ma_num; j < i; j++)
                {
                    sum += PriceData.close[j];
                }
                res.Add(sum/Convert.ToDouble(ma_num));
            }
            return res;
        }

    }
}
