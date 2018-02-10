using System.Threading.Tasks;

namespace Trader.Backfill
{
    public interface IBackFill
    {
        Task Execute(BackFillOptions options);
    }
}