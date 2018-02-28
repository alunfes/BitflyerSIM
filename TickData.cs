using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitflyerSIM
{
    class TickData
    {
        public static List<DateTime> time;
        public static List<double> price;
        public static List<double> volume;

        public static void initialize()
        {
            time = new List<DateTime>();
            price = new List<double>();
            volume = new List<double>();
        }
    }
}
