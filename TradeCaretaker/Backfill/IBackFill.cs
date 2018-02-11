using System.Threading.Tasks;

namespace TradeCaretaker.Backfill
{
    public interface IBackFill
    {
        Task Execute(BackFillOptions options);
    }
}