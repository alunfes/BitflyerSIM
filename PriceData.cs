using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitflyerSIM
{
    class PriceData
    {
        public static List<DateTime> date;
        public static List<double> open;
        public static List<double> high;
        public static List<double> low;
        public static List<double> close;
        public static List<double> volume;

        public static void initialize()
        {
            date = new List<DateTime>();
            open = new List<double>();
            high = new List<double>();
            low = new List<double>();
            close = new List<double>();
            volume = new List<double>();
        }
    }
}
