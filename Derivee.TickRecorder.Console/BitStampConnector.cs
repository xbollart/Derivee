﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derivee.TickRecorder.Console
{
    internal class BitStampConnector
    {
        private string _url = "https://www.bitstamp.net";
        private RestClient _client;

        public BitStampConnector()
        {
            _client = new RestClient(_url);
        }

        public void GetPrice(string pair)
        {

            var request = new RestRequest($"/api/v2/ticker/{pair}", Method.GET);

            IRestResponse response = _client.Execute(request);
            System.Console.WriteLine("test");
         //   return JsonConvert.DeserializeObject<TickerResponse>(response.Content);
        }
    }
}
