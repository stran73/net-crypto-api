﻿

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TickerMonitor
{

    public class BitstampTicker
    {

        [JsonProperty("high")]
        public string High { get; set; }

        [JsonProperty("last")]
        public string Last { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("bid")]
        public string Bid { get; set; }

        [JsonProperty("vwap")]
        public string Vwap { get; set; }

        [JsonProperty("volume")]
        public string Volume { get; set; }

        [JsonProperty("low")]
        public string Low { get; set; }

        [JsonProperty("ask")]
        public string Ask { get; set; }

        [JsonProperty("open")]
        public string Open { get; set; }
    }

}
