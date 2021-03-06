﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TickerMonitor
{
    public class BitstampOrderBook
    {
        public string timestamp { get; set; }
        public List<List<string>> bids { get; set; }
        public List<List<string>> asks { get; set; }
    }
}
