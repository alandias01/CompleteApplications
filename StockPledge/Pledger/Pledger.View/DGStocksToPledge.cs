﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pledger.View
{
    public class DGStocksToPledge
    {
        public string cusip { get; set; }
        public int QuantityToPledge { get; set; }
        public decimal? Price { get; set; }
    }
}
