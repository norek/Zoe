using System.Threading.Tasks;

namespace Zoe.Backfill
{
    public interface IBackFill
    {
        Task Execute(BackFillOptions options);
    }
}