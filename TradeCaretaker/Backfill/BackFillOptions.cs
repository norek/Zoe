using System;

namespace TradeCaretaker.Backfill
{
    public struct BackFillOptions
    {
        public BackFillOptions(DateTime dateFrom, DateTime dateTo, string asset) : this()
        {
            DateFrom = dateFrom;
            DateTo = dateTo;
            Asset = asset;
        }

        public DateTime DateFrom { get; }
        public DateTime DateTo { get; }
        public string Asset { get; }
    }
}