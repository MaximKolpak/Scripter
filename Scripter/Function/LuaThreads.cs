using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scripter.Function
{
    class LuaThreads
    {
        public void ProgrammSleep(double mlsec)
        {
            int count = (int)mlsec;
            Thread.Sleep(count);
        }
    }
}
