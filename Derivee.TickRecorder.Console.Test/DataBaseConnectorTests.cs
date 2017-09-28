using System;
using NUnit.Framework;


namespace Derivee.TickRecorder.Console.Test
{
    [TestFixture]
    public class DataBaseConnectorTests
    {

        [Test]
        public void InsertMethodShoudInsertMarketDataIntoDataBase()
        {
            var dbConnector = new DataBaseConnector();
            var mktData = new MarketData() {last = 0.1,high=0.2,timestamp =12,bid=0.3,vwap=0.4,volume=13,low =0.5,ask=0.6,open=0.7 };
            dbConnector.InsertInto(mktData,"btceur");


            Assert.IsTrue(true);

        }
    }
}
