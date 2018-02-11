using System;

namespace TradeCaretaker.Sim
{
    public struct SimulationOptions
    {
        public string Asset { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int PeriodLength { get; set; }
    }
}