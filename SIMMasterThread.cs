using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace BitflyerSIM
{
    class SIMMasterThread
    {
        public static void startMasterThread()
        {
            thread();
        }

        private static void thread()
        {
            Thread th = new Thread(ReadData.readPriceData);
            th.Start();
            th.Join();

            

        }
    }
}
