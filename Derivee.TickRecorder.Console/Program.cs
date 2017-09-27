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
                string ccy = "btcusd";
                Thread.Sleep(2000);
                var mktData = bitStamp.GetPrice(ccy);

                System.Console.WriteLine($"{ccy} high: {mktData.high}");
                System.Console.WriteLine($"{ccy} last: {mktData.last}");
                System.Console.WriteLine($"{ccy} timestamp: {mktData.timestamp}");
                System.Console.WriteLine($"{ccy} bid: {mktData.bid}");
                System.Console.WriteLine($"{ccy} vwap: {mktData.vwap}");
                System.Console.WriteLine($"{ccy} volume: {mktData.volume}");
                System.Console.WriteLine($"{ccy} low: {mktData.low}");
                System.Console.WriteLine($"{ccy} ask: {mktData.ask}");
                System.Console.WriteLine($"{ccy} open: {mktData.open}");
            }
        }
    }
}
