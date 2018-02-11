using System.Threading.Tasks;

namespace TradeCaretaker.Sim
{
    public interface ISimulation
    {
        Task Run(SimulationOptions options);
    }
}