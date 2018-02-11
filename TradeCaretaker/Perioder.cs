using System;
using System.Linq;
using TradeCaretaker.Exchanges;

namespace TradeCaretaker
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

        public void Periodify(Trade[] trades, DateTime dateFrom, DateTime dateTo,
            int periodLength, Action<Trade> onPeriod)
        {
            var diffInMinutes = (int)(dateTo - dateFrom).TotalMinutes;

            var numberOfPeriods = (int)Math.Ceiling((double)diffInMinutes / periodLength);

            if (numberOfPeriods == 0)
            {
                numberOfPeriods = 1;
            }

            int startIndex = 0;

            Trade periodedTrade = new Trade();

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
                            onPeriod(periodedTrade);
                            startIndex = j;
                            break;
                        }
                    }
                    else
                    {
                        onPeriod(periodedTrade);
                        startIndex = j;
                        break;
                    }
                }

                dateFrom = dateFrom.AddMinutes(periodLength);
            }
        }

        public void Periodify_withoutDateTime(Trade[] trades, int dateFrom, int dateTo,
            int periodLength, Action<Trade> onPeriod)
        {
            var diffInMinutes = dateTo - dateFrom;

            var numberOfPeriods = (int)Math.Ceiling((double)diffInMinutes / periodLength);

            if (numberOfPeriods == 0)
            {
                numberOfPeriods = 1;
            }

            int startIndex = 0;

            for (int i = 0; i < numberOfPeriods; i++)
            {
                var periodTo = dateFrom + periodLength;

                Trade periodedTrade = new Trade();

                for (int j = startIndex; j < trades.Length; j++)
                {
                    var trade = trades[j];

                    if (trade.Date.TimeOfDay.TotalSeconds > dateFrom && trade.Date.TimeOfDay.TotalSeconds < periodTo)
                    {
                        periodedTrade = trade;

                        if (j == trades.Length - 1)
                        {
                            onPeriod(periodedTrade);
                            startIndex = j;
                            break;
                        }
                    }
                    else
                    {
                        onPeriod(periodedTrade);
                        startIndex = j;
                        break;
                    }
                }

                dateFrom = dateFrom + periodLength;
            }
        }
    }
}