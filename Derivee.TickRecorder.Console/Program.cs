using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Derivee.TickRecorder.Console
{
    class Program
    {
        static string _recordingDirectoryPath = "PriceRecording";
        static string _sep = ",";

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

                    DisplayMarketData(mktData, pair);
                    WriteToCsv(mktData, pair);
                    Thread.Sleep(2000);
                }
            }
        }

        private static void DisplayMarketData(MarketData mktData, string pair)
        {
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
        }

        private static void WriteToCsv(MarketData mktData, string pair)
        {
            string date = DateTime.UtcNow.ToString("yyyy-MM-dd");
            string fileName = $"{date}_{pair}.csv";
            string folderPath = Path.Combine(_recordingDirectoryPath, date);
            string filePath = Path.Combine(folderPath, fileName);

            if(!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            using (var wr = new StreamWriter(filePath, true))
            {
                wr.WriteLine(mktData.timestamp + _sep + pair + _sep + mktData.bid + 
                    _sep + mktData.ask + _sep + mktData.last + _sep + mktData.high + 
                    _sep + mktData.low + _sep + mktData.volume + _sep + mktData.vwap);
            }
        }
    }
}
