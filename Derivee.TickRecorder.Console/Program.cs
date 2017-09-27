using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Derivee.TickRecorder.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var bitStamp = new BitStampConnector();

            while (true)
            {
                Thread.Sleep(1000);
                bitStamp.GetPrice("btcusd");
            }
        }
    }
}
