﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ExternalServices
{

    public class BitstampOrderSend
    {

        [JsonProperty("event")]
        public string Event { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("pair")]
        public string Pair { get; set; }

        [JsonProperty("prec")]
        public string Prec { get; set; }

        [JsonProperty("freq")]
        public string Freq { get; set; }
    }

}
