﻿using System;

namespace Zoe.Messages
{
    public struct PeriodClosed
    {
        public DateTime DateTime { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
    }
}
