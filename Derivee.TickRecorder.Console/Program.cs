using System;
using System.Configuration;
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
        private static readonly string _recordingDirectoryPath = ConfigurationManager.AppSettings["OutputPath"];
        private static readonly string _sep = ConfigurationManager.AppSettings["Separator"];
        private static readonly int _period = int.Parse(ConfigurationManager.AppSettings["RefreshPeriod"]);

        static void Main(string[] args)
        {
            if (_period < 1000)
            {
                System.Console.WriteLine("Please restart app with a Period at least equal to: 1000 (ms)");
                return;
            }

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
                    Thread.Sleep(_period);
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
