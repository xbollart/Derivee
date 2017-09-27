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
            // 12 pairs
            string[] pairs = { "btcusd", "btceur", "eurusd","xrpusd","xrpeur","xrpbtc","ltcusd","ltceur","ltcbtc","ethusd","etheur","ethbtc" };
            while (true)
            {
                foreach (var pair in pairs)
                {
                    var mktData = bitStamp.GetPrice(pair);

                    if (mktData == null)
                    {
                        System.Console.WriteLine($"Issue while Loading {pair}");
                    }
                    else
                    {
                        System.Console.WriteLine(pair);
                        System.Console.WriteLine($"high: {mktData.high}");
                        System.Console.WriteLine($"last: {mktData.last}");
                        System.Console.WriteLine($"timestamp: {mktData.timestamp}");
                        System.Console.WriteLine($"bid: {mktData.bid}");
                        System.Console.WriteLine($"vwap: {mktData.vwap}");
                        System.Console.WriteLine($"volume: {mktData.volume}");
                        System.Console.WriteLine($"low: {mktData.low}");
                        System.Console.WriteLine($"ask: {mktData.ask}");
                        System.Console.WriteLine($"open: {mktData.open}");
                        System.Console.WriteLine("");
                    }

                    Thread.Sleep(2000);
                }
            }
        }
    }
}
