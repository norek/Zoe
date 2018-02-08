using System;

namespace Trader.Backfill
{
    struct BackFillOptions
    {
        public BackFillOptions(DateTime dateFrom, DateTime dateTo, string asset) : this()
        {
            DateFrom = dateFrom;
            DateTo = dateTo;
            Asset = asset;
        }

        public DateTime DateFrom { get; private set; }
        public DateTime DateTo { get; private set; }
        public string Asset { get; private set; }
    }
}
