using System;
using System.Linq;
using System.Threading.Tasks;
using Zoe.Exchanges;

namespace Zoe
{
    public class Perioder
    {
        public void Periodify_WithSomeLinq(Trade[] trades, DateTime dateFrom, DateTime dateTo,
            int periodLength, Action<Trade> onPeriod)
        {
            var diffInMinutes = (int)(dateTo - dateFrom).TotalMinutes;

            var numberOfPeriods = (int)Math.Ceiling((double)diffInMinutes / periodLength);

            if (numberOfPeriods == 0)
            {
                numberOfPeriods = 1;
            }

            DateTime periodTo;
            for (int i = 0; i < numberOfPeriods; i++)
            {
                periodTo = dateFrom.AddMinutes(periodLength);

                var lastTrade = trades.LastOrDefault(trade => trade.Date > dateFrom && trade.Date < periodTo);

                onPeriod(lastTrade);

                dateFrom = dateFrom.AddMinutes(periodLength);
            }
        }

        public async Task Periodify(Trade[] trades, DateTime dateFrom, DateTime dateTo,
            int periodLength, Func<DateTime, Trade, Task> onPeriod)
        {
            var diffInMinutes = (int)(dateTo - dateFrom).TotalMinutes;

            var numberOfPeriods = (int)Math.Ceiling((double)diffInMinutes / periodLength);

            if (numberOfPeriods == 0)
            {
                numberOfPeriods = 1;
            }

            int startIndex = 0;

            Trade periodedTrade = null;

            for (int i = 0; i < numberOfPeriods; i++)
            {
                var periodTo = dateFrom.AddMinutes(periodLength);

                for (int j = startIndex; j < trades.Length; j++)
                {
                    var trade = trades[j];

                    if (trade.Date > dateFrom && trade.Date < periodTo)
                    {
                        periodedTrade = trade;

                        if (j == trades.Length - 1)
                        {
                            await onPeriod(periodTo, periodedTrade);
                            startIndex = j;
                            break;
                        }
                    }
                    else
                    {
                        await onPeriod(periodTo, periodedTrade);
                        startIndex = j;
                        break;
                    }
                }

                dateFrom = dateFrom.AddMinutes(periodLength);
            }
        }
    }
}