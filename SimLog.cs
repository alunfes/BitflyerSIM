using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitflyerSIM
{
    class SimLog
    {
        private List<List<string>> sim_logs;
        private List<string> log;
        private List<int> log_id;
        private int current_id;

        public SimLog()
        {
            initialize();
        }

        public void initialize()
        {
            sim_logs = new List<List<string>>();
            log = new List<string>();
            log_id = new List<int>();
            current_id = -1;
        }

        public void addLog(int i, string log_info)
        {
            if (current_id != i)
            {
                sim_logs.Add(log);
                log_id.Add(current_id);
                current_id = i;
                log = new List<string>();
                log.Add(log_info);
            }
            else
            {
                log.Add(log_info);
            }
        }

    }
}
