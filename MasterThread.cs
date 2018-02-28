using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace BitflyerSIM
{
    class MasterThread
    {
        public static void startMasterThread()
        {
            thread();
        }

        private static void thread()
        {
            Thread th = new Thread(ReadData.readTickData);
            th.Start();
            th.Join();

            Thread th2 = new Thread(TickConverter.converterTo1M);
            th2.Start();
            th2.Join();

            Thread th3 = new Thread(new ParameterizedThreadStart(WriteConvertedData.writeData));
            th3.Start("1m.csv");
            th3.Join();
            

        }
    }
}
